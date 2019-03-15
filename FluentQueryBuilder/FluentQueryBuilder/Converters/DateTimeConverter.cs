using System;

namespace FluentQueryBuilder.Converters
{
    public class DateTimeConverter : IPropertyConverter
    {
        public object Convert(string source, params object[] parameters)
        {
            return DateTime.Parse(source);
        }

        public string ConvertBack(object source, params object[] parameters)
        {
            DateTime value = (DateTime) source;
            return value.ToString();
        }
    }
}
