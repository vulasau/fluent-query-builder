using System;
using FluentQueryBuilder.Converters;

namespace FluentQueryBuilder.Application.Converters
{
    public class DateConverter: IPropertyConverter
    {
        public object Convert(string source)
        {
            var blocks = source.Split('-');
            var year = int.Parse(blocks[0]);
            var month = int.Parse(blocks[1]);
            var day = int.Parse(blocks[2]);

            return new DateTime(year, month, day);
        }

        public string ConvertBack(object source)
        {
            var date = (DateTime) source;

            return string.Format("{0}-{1}-{2}", date.Year, date.Month, date.Day);
        }
    }
}
