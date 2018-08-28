using System;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity(MODEL_NAME)]
    public class NamedFluentModelBase
    {
        public const string MODEL_NAME = "model";

        public const string BOOLEAN_PROPERTY_NAME = "boolean";
        public const string DATE_PROPERTY_NAME = "date";

        public static readonly bool BOOLEAN_VALUE = true;
        public static readonly DateTime DATE_VALUE = new DateTime(2017, 1, 1);

        [FluentProperty(BOOLEAN_PROPERTY_NAME)]
        public bool BooleanProperty { get; set; }

        [FluentProperty(DATE_PROPERTY_NAME)]
        public DateTime DateProperty { get; set; }

        public NamedFluentModelBase()
        {
            BooleanProperty = BOOLEAN_VALUE;
            DateProperty = DATE_VALUE;
        }
    }
}
