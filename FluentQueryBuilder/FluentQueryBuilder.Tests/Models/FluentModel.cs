using System;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity]
    public class FluentModel
    {
        public const string MODEL_NAME = "FluentModel";
        public const string BOOLEAN_PROPERTY_NAME = "BooleanProperty";
        public const string DATE_PROPERTY_NAME = "DateProperty";
        public const string DOUBLE_PROPERTY_NAME = "DoubleProperty";
        public const string INTEGER_PROPERTY_NAME = "IntegerProperty";
        public const string OBJECT_PROPERTY_NAME = "ObjectProperty";
        public const string READONLY_PROPERTY_NAME = "ReadonlyProperty";
        public const string CONDITIONED_PROPERTY_NAME = "ConditionedProperty";
        public const string CONDITIONED_REVERSE_PROPERTY_NAME = "ConditionedReverseProperty";

        public static readonly bool BOOLEAN_VALUE = true;
        public static readonly DateTime DATE_VALUE = new DateTime(2017, 1, 1);
        public static readonly double DOUBLE_VALUE = 100.5;
        public static readonly int INTEGER_VALUE = 100;
        public static readonly object OBJECT_VALUE = "object";
        public static readonly string READONLY_VALUE = "readonly";
        public static readonly string CONDITIONED_VALUE = "conditioned";
        public static readonly string CONDITIONED_REVERSE_VALUE = "conditioned reverse";

        [FluentProperty]
        public bool BooleanProperty { get; set; }
        
        [FluentProperty]
        public DateTime DateProperty { get; set; }

        [FluentProperty]
        public double DoubleProperty { get; set; }

        [FluentProperty]
        public int IntegerProperty { get; set; }

        [FluentProperty]
        public object ObjectProperty { get; set; }

        [FluentProperty(isReadonly: true)]
        public string ReadonlyProperty { get; set; }

        [FluentProperty]
        [Condition("Hola")]
        public string ConditionedProperty { get; set; }

        [FluentProperty]
        [Condition("Hola", true)]
        public string ConditionedReverseProperty { get; set; }

        public FluentModel()
        {

        }

        public FluentModel(bool withValues)
        {
            if (withValues)
            {
                BooleanProperty = BOOLEAN_VALUE;
                DateProperty = DATE_VALUE;
                DoubleProperty = DOUBLE_VALUE;
                IntegerProperty = INTEGER_VALUE;
                ObjectProperty = OBJECT_VALUE;
                ReadonlyProperty = READONLY_VALUE;
                ConditionedProperty = CONDITIONED_VALUE;
                ConditionedReverseProperty = CONDITIONED_REVERSE_VALUE;
            }
        }
    }
}