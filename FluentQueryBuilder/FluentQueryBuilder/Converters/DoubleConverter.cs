namespace FluentQueryBuilder.Converters
{
    public class DoubleConverter: IPropertyConverter
    {
        public object Convert(string source)
        {
            return double.Parse(source);
        }

        public string ConvertBack(object source)
        {
            double value = (double)source;
            return value.ToString();
        }
    }
}
