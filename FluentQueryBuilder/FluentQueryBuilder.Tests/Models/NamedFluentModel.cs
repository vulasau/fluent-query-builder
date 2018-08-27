using System;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity(MODEL_NAME)]
    public class NamedFluentModel
    {
        public const string MODEL_NAME = "model";
        public const string BOOLEAN_PROPERTY_NAME = "boolean";
        public const string DATE_PROPERTY_NAME = "date";
        public const string DOUBLE_PROPERTY_NAME = "double";
        public const string INTEGER_PROPERTY_NAME = "integer";
        public const string OBJECT_PROPERTY_NAME = "object";

        public static readonly bool BOOLEAN_VALUE = true;
        public static readonly DateTime DATE_VALUE = new DateTime(2017, 1, 1);
        public static readonly double DOUBLE_VALUE = 100.5;
        public static readonly int INTEGER_VALUE = 100;
        public static readonly object OBJECT_VALUE = "object";

        [FluentProperty(BOOLEAN_PROPERTY_NAME)]
        public bool BooleanProperty { get; set; }

        [FluentProperty(DATE_PROPERTY_NAME)]
        public DateTime DateProperty { get; set; }

        [FluentProperty(DOUBLE_PROPERTY_NAME)]
        public double DoubleProperty { get; set; }

        [FluentProperty(INTEGER_PROPERTY_NAME)]
        public int IntegerProperty { get; set; }

        [FluentProperty(OBJECT_PROPERTY_NAME)]
        public object ObjectProperty { get; set; }

        public NamedFluentModel()
        {
            BooleanProperty = BOOLEAN_VALUE;
            DateProperty = DATE_VALUE;
            DoubleProperty = DOUBLE_VALUE;
            IntegerProperty = INTEGER_VALUE;
            ObjectProperty = OBJECT_VALUE;
        }
    }
}
