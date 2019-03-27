using System;
using System.Linq;
using System.Linq.Expressions;
using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Configuration;

namespace FluentQueryBuilder.Extensions
{
    public static class ExpressionExtensions
    {
        private static readonly IExpressionTypeTransformer _expressionTypeTransformer;
        private static readonly IStringificationRulesResolver _stringificationRulesResolver;
        private static readonly INullComparisonResolver _nullComparisonResolver;

        static ExpressionExtensions()
        {
            _expressionTypeTransformer = ExpressionParserConfiguration.ExpressionTypeTransformer;
            _stringificationRulesResolver = ExpressionParserConfiguration.StringificationRulesResolver;
            _nullComparisonResolver = ExpressionParserConfiguration.NullComparisonResolver;
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

            string left, right = null;

            var preprocessed = PreProcess<T>(binaryExpression, out left, out right);
            if (!preprocessed)
            {
                left = ParseExpression<T>(binaryExpression.Left);
                if (binaryExpression.Left is BinaryExpression)
                    left = left.WrapWithBrackets();

                right = ParseExpression<T>(binaryExpression.Right);
                if (binaryExpression.Right is BinaryExpression)
                    right = right.WrapWithBrackets();
            }

            var node = _expressionTypeTransformer.Transform(binaryExpression.NodeType);

            if (left == null || right == null)
                return ResolverNullComparison(left, right, binaryExpression.NodeType);

            return string.Format("{0} {1} {2}", left, node, right);
        }

        private static string Process(this ConstantExpression constantExpression)
        {
            var stringify = _stringificationRulesResolver.RequiresStringification(constantExpression.Type);
            var value = constantExpression.Value;

            var converter = ConverterExtensions.GetConverter(null, constantExpression.Type);
            var valueString = converter.ConvertBack(value);

            if (valueString == null)
                return null;

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

        private static bool PreProcess<T>(this BinaryExpression binaryExpression, out string enumMemberName, out string enumCompareValue)
        {
            Type type = null;
            Type memberType = null;
            string memberName = null;
            string valueInterpretationString = null;

            if (binaryExpression.Left is UnaryExpression && ((UnaryExpression)binaryExpression.Left).Operand is MemberExpression)
            {
                type = binaryExpression.Left.Type;
                memberType = ((UnaryExpression)binaryExpression.Left).Operand.Type;
                memberName = ParseExpression<T>(binaryExpression.Left);
                valueInterpretationString = ParseExpression<T>(binaryExpression.Right);
            }
            else if (binaryExpression.Right is UnaryExpression && ((UnaryExpression)binaryExpression.Right).Operand is MemberExpression)
            {
                type = binaryExpression.Right.Type;
                memberType = ((UnaryExpression)binaryExpression.Right).Operand.Type;
                memberName = ParseExpression<T>(binaryExpression.Right);
                valueInterpretationString = ParseExpression<T>(binaryExpression.Left);
            }

            if (memberType != null && memberType.IsEnum)
            {
                var valueInterpretation = int.Parse(valueInterpretationString);
                var value = Enum.ToObject(memberType, valueInterpretation);

                var prop = typeof(T).GetFluentProperty(memberName);

                object[] converterParameters;
                var converter = prop.GetConverter(out converterParameters);
                var valueString = converter.ConvertBack(value, converterParameters);

                var stringify = _stringificationRulesResolver.RequiresStringification(memberType);
                if (valueString != null && stringify)
                    valueString = valueString.WrapWithQuotes();

                enumMemberName = memberName;
                enumCompareValue = valueString;
                return true;
            }

            enumMemberName = null;
            enumCompareValue = null;
            return false;
        }


        private static string CompileExpression(Expression expression)
        {
            var stringify = _stringificationRulesResolver.RequiresStringification(expression.Type);

            var unaryExpression = Expression.Convert(expression, typeof(object));
            var getterExpression = Expression.Lambda<Func<object>>(unaryExpression);
            var getter = getterExpression.Compile();
            var value = getter();

            var converter = ConverterExtensions.GetConverter(null, expression.Type);
            var valueString = converter.ConvertBack(value);

            if (valueString == null)
                return null;

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
    }
}
