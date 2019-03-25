﻿using System;
using System.Linq;
using System.Linq.Expressions;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Extensions
{
    public static class ExpressionExtensions
    {
        private static readonly IExpressionTypeTransformer _expressionTypeTransformer;
        private static readonly IStringificationRulesResolver _stringificationRulesResolver;
        private static readonly NullComparisonResolverBase _nullComparisonResolver;
        private static readonly IConverterResolver _converterResolver;
        private static readonly IConverterFactory _converterFactory;

        static ExpressionExtensions()
        {
            _expressionTypeTransformer = ExpressionParserConfiguration.ExpressionTypeTransformer;
            _stringificationRulesResolver = ExpressionParserConfiguration.StringificationRulesResolver;
            _nullComparisonResolver = ExpressionParserConfiguration.NullComparisonResolver;
            _converterResolver = ObjectMapperConfiguration.ConverterResolver;
            _converterFactory = ObjectMapperConfiguration.ConverterFactory;
        }

        public static string Parse<T>(this Expression<Func<T, bool>> predicate)
        {
            return ParseExpression<T>(predicate.Body);
        }

        public static string ParseMemberExpression<T, TOut>(this Expression<Func<T, TOut>> predicate)
        {
            var expression = predicate.Body;

            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression) expression;
                return memberExpression.Process<T>();
            }

            throw new ArgumentException("Parameter 'expression' should be of type 'MemberExpression'", "expression");
        }

        private static string ParseExpression<T>(Expression expression)
        {
            if (expression is ConstantExpression)
            {
                var constantExpression = (ConstantExpression)expression;
                return constantExpression.Process();
            }
            else if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Process<T>();
            }
            else if (expression is MethodCallExpression)
            {
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Process();
            }
            else if (expression is BinaryExpression)
            {
                var binaryExpression = (BinaryExpression)expression;
                return binaryExpression.Process<T>();
            }
            else if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression) expression;

                if (unaryExpression.Operand is MemberExpression)
                    return ParseExpression<T>((MemberExpression)unaryExpression.Operand);
                else
                    return CompileExpression(unaryExpression);
            }
            else if (expression is NewExpression)
            {
                var newExpression = (NewExpression) expression;
                return CompileExpression(newExpression);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Expression of type '{0}' is not supported.", expression.GetType().ToString()));
            }
        }

        private static bool TryProcessEnumTypeComparisonExpression<T>(this BinaryExpression binaryExpression, out string enumMemberName, out string enumComparableValue)
        {
            MemberExpression enumMemberExpression;
            Expression enumValueExpression;

            if(binaryExpression.Left is UnaryExpression && ((UnaryExpression)binaryExpression.Left).NodeType == ExpressionType.Convert)
            {
                enumMemberExpression = ((UnaryExpression)binaryExpression.Left).Operand as MemberExpression;
                enumValueExpression = binaryExpression.Right;
            }
            else if(binaryExpression.Right is UnaryExpression && ((UnaryExpression)binaryExpression.Right).NodeType == ExpressionType.Convert)
            {
                enumMemberExpression = ((UnaryExpression)binaryExpression.Right).Operand as MemberExpression;
                enumValueExpression = binaryExpression.Left;
            }
            else
            {
                enumMemberName = null;
                enumComparableValue = null;
                return false;
            }

            var enumType = enumMemberExpression.Type;

            // Easy: Get enum comparison expression member name 
            enumMemberName = ParseExpression<T>(enumMemberExpression);

            // Hard: Get enum compariosn expression comparable value
            var enumValueString = ParseExpression<T>(enumValueExpression);
            var enumValueInterpretation = int.Parse(enumValueString);
            var enumValue = Enum.ToObject(enumType, enumValueInterpretation);

            var memberProperty = typeof(T).GetProperty(enumMemberExpression.Member.Name);

            var converterAttribute = memberProperty.GetCustomAttributes(typeof(ConverterAttribute), false).SingleOrDefault() as ConverterAttribute;
            var converterType = converterAttribute == null ? null : converterAttribute.Type;
            var converterParameters = converterAttribute == null ? new object[0] : converterAttribute.Parameters;
            var converter = GetConverter(converterType, memberProperty.PropertyType);

            var stringify = _stringificationRulesResolver.RequiresStringification(converterAttribute.ValueType);

            enumComparableValue = converter.ConvertBack(enumValue, converterParameters);
            enumComparableValue = stringify ? WrapWithQuotes(enumComparableValue) : enumComparableValue;

            return true;
        }

        private static string Process<T>(this BinaryExpression binaryExpression)
        {
            try
            {
                // Trying to evaluate
                return CompileExpression(binaryExpression);
            }
            catch
            {
                // Ignoring if not evaluatable and parsing
            }

            var left = ParseExpression<T>(binaryExpression.Left);
            if (binaryExpression.Left is BinaryExpression)
                left = left.WrapWithBrackets();

            var right = ParseExpression<T>(binaryExpression.Right);
            if (binaryExpression.Right is BinaryExpression)
                right = right.WrapWithBrackets();

            var node = _expressionTypeTransformer.Transform(binaryExpression.NodeType);

            if (left == null || right == null)
                return ResolverNullComparison(left, right, binaryExpression.NodeType);

            return string.Format("{0} {1} {2}", left, node, right);
        }

        private static string Process(this ConstantExpression constantExpression)
        {
            var stringify = _stringificationRulesResolver.RequiresStringification(constantExpression.Type);
            var value = constantExpression.Value;

            var converter = _converterResolver.Resolve(constantExpression.Type);
            var valueString = converter.ConvertBack(value);

            return stringify ? valueString.WrapWithQuotes() : valueString;
        }

        private static string Process(this MethodCallExpression methodCallExpression)
        {
            return CompileExpression(methodCallExpression);
        }

        private static string Process<T>(this MemberExpression memberExpression)
        {
            if (memberExpression.Member.ReflectedType.IsAssignableFrom(typeof(T)) && !memberExpression.ToString().StartsWith("value"))
                return ExtractMemberName<T>(memberExpression);
            else
                return CompileExpression(memberExpression);
        }



        private static string CompileExpression(Expression expression)
        {
            var stringify = _stringificationRulesResolver.RequiresStringification(expression.Type);

            var unaryExpression = Expression.Convert(expression, typeof(object));
            var getterExpression = Expression.Lambda<Func<object>>(unaryExpression);
            var getter = getterExpression.Compile();
            var value = getter();

            var converter = _converterResolver.Resolve(expression.Type);
            var valueString = converter.ConvertBack(value);

            return stringify ? valueString.WrapWithQuotes() : valueString;
        }

        private static string ExtractMemberName<T>(MemberExpression memberExpression)
        {
            var memberName = memberExpression.Member.Name;
            var memberType = typeof(T);
            var memberProperty = memberType.GetProperty(memberName);
            var fluentPropertyAttribute = memberProperty.GetCustomAttributes(typeof(FluentPropertyAttribute), false).SingleOrDefault() as FluentPropertyAttribute;
            var propertyName = fluentPropertyAttribute != null ? fluentPropertyAttribute.Name ?? memberName : memberName;

            return propertyName;
        }



        private static string ResolverNullComparison(string left, string right, ExpressionType nodeType)
        {
            if (nodeType != ExpressionType.Equal && nodeType != ExpressionType.NotEqual)
                throw new InvalidOperationException("Only 'Equal' and 'NotEqual' operations are possible in 'Null' comparison expressions.");

            if (left == null && right == null)
                throw new InvalidOperationException("Only one of binary expression nodes can be null in 'Null' comparison expression.");

            var isNegative = nodeType == ExpressionType.NotEqual;

            if (left == null)
                return _nullComparisonResolver.GenerateExpression(right, isNegative);
            else
                return _nullComparisonResolver.GenerateExpression(left, isNegative);
        }

        private static string WrapWithQuotes(this string source)
        {
            return string.Format("'{0}'", source);
        }

        private static string WrapWithBrackets(this string source)
        {
            return string.Format("({0})", source);
        }

        private static IPropertyConverter GetConverter(Type converterType, Type returnType)
        {
            if (converterType != null)
                return _converterFactory.CreateConverter(converterType);

            return _converterResolver.Resolve(returnType);
        }
    }
}
