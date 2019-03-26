using FluentQueryBuilder.Attributes;
using FluentQueryBuilder.Tests.Models.Converters;
using FluentQueryBuilder.Tests.Models.Enums;

namespace FluentQueryBuilder.Tests.Models
{
    [FluentEntity]
    public class ConvertableModel
    {
        public const string MODEL_NAME = "ConvertableModel";
        public const string CONVERTABLE_PROPERTY_NAME = "ConvertableProperty_c";

        public static readonly string CONVERTABLE_STRING_VALUE = EnumValueConverter.FIRST_VALUE;
        public static readonly EnumValue CONVERTABLE_ENUM_VALUE = EnumValue.FirstValue;

        [FluentProperty("ConvertableProperty_c")]
        [Converter(typeof(EnumValueConverter), typeof(string))]
        public EnumValue ConvertableProperty { get; set; }

        public ConvertableModel()
        {

        }

        public ConvertableModel(bool withValues)
        {
            if (withValues)
            {
                ConvertableProperty = CONVERTABLE_ENUM_VALUE;
            }
        }
    }
}