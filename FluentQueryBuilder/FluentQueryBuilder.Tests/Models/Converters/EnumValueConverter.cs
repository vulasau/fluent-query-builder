using System.Collections.Generic;
using System.Linq;
using FluentQueryBuilder.Converters;
using FluentQueryBuilder.Tests.Models.Enums;

namespace FluentQueryBuilder.Tests.Models.Converters
{
    public class EnumValueConverter : IPropertyConverter
    {
        public const string FIRST_VALUE = "First Value";
        public const string SECOND_VALUE = "Second Value";

        private readonly Dictionary<string, EnumValue> _map = new Dictionary<string, EnumValue>()
        {
            { FIRST_VALUE, EnumValue.FirstValue },
            { SECOND_VALUE, EnumValue.SecondValue }
        };

        public object Convert(string source, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(source))
                return EnumValue.Unknown;

            if (!_map.ContainsKey(source))
                return EnumValue.Unknown;

            return _map[source];
        }

        public string ConvertBack(object source, params object[] parameters)
        {
            var value = (EnumValue)source;

            if (!_map.Any(x => x.Value == value))
                return null;

            return _map.First(x => x.Value == value).Key;
        }
    }
}
