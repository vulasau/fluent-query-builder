using System;

namespace FluentQueryBuilder.Converters
{
    public class DateTimeConverter : IPropertyConverter
    {
        public object Convert(string source)
        {
            return DateTime.Parse(source);
        }

        public string ConvertBack(object source)
        {
            DateTime value = (DateTime) source;
            return value.ToString();
        }
    }
}
