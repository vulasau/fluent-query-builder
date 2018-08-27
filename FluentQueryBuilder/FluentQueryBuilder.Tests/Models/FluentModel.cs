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

        public static readonly bool BOOLEAN_VALUE = true;
        public static readonly DateTime DATE_VALUE = new DateTime(2017, 1, 1);
        public static readonly double DOUBLE_VALUE = 100.5;
        public static readonly int INTEGER_VALUE = 100;
        public static readonly object OBJECT_VALUE = "object";

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

        public FluentModel()
        {
            BooleanProperty = BOOLEAN_VALUE;
            DateProperty = DATE_VALUE;
            DoubleProperty = DOUBLE_VALUE;
            IntegerProperty = INTEGER_VALUE;
            ObjectProperty = OBJECT_VALUE;
        }
    }
}
