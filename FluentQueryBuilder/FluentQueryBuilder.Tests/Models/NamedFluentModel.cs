using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity(MODEL_NAME)]
    public class NamedFluentModel: NamedFluentModelBase
    {
        public const string DOUBLE_PROPERTY_NAME = "double";
        public const string INTEGER_PROPERTY_NAME = "integer";
        public const string OBJECT_PROPERTY_NAME = "object";

        public static readonly double DOUBLE_VALUE = 100.5;
        public static readonly int INTEGER_VALUE = 100;
        public static readonly object OBJECT_VALUE = "object";


        [FluentProperty(DOUBLE_PROPERTY_NAME)]
        public double DoubleProperty { get; set; }

        [FluentProperty(INTEGER_PROPERTY_NAME)]
        public int IntegerProperty { get; set; }

        [FluentProperty(OBJECT_PROPERTY_NAME)]
        public object ObjectProperty { get; set; }

        public NamedFluentModel() : base()
        {
            DoubleProperty = DOUBLE_VALUE;
            IntegerProperty = INTEGER_VALUE;
            ObjectProperty = OBJECT_VALUE;
        }
    }
}
