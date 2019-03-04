using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity(MODEL_NAME)]
    public class NamedFluentModel: NamedFluentModelBase
    {
        public const string DOUBLE_PROPERTY_NAME = "double";
        public const string INTEGER_PROPERTY_NAME = "integer";
        public const string OBJECT_PROPERTY_NAME = "object";
        public const string READONLY_PROPERTY_NAME = "readonly";
        public const string CONDITIONED_PROPERTY_NAME = "conditioned";
        public const string CONDITIONED_REVERSE_PROPERTY_NAME = "conditioned_reverse";

        public static readonly double DOUBLE_VALUE = 100.5;
        public static readonly int INTEGER_VALUE = 100;
        public static readonly object OBJECT_VALUE = "object";
        public static readonly string READONLY_VALUE = "readonly";
        public static readonly string CONDITIONED_VALUE = "conditioned";
        public static readonly string CONDITIONED_REVERSE_VALUE = "conditioned reverse";

        [FluentProperty(DOUBLE_PROPERTY_NAME)]
        public double DoubleProperty { get; set; }

        [FluentProperty(INTEGER_PROPERTY_NAME)]
        public int IntegerProperty { get; set; }

        [FluentProperty(OBJECT_PROPERTY_NAME)]
        public object ObjectProperty { get; set; }

        [FluentProperty(READONLY_PROPERTY_NAME, isReadonly: true)]
        public string ReadonlyProperty { get; set; }

        [FluentProperty(CONDITIONED_PROPERTY_NAME)]
        [Condition("Hola")]
        public string ConditionedProperty { get; set; }

        [FluentProperty(CONDITIONED_REVERSE_PROPERTY_NAME)]
        [Condition("Hola", true)]
        public string ConditionedReverseProperty { get; set; }

        public NamedFluentModel() : base()
        {

        }

        public NamedFluentModel(bool withValues) : base(withValues)
        {
            if (withValues)
            {
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
