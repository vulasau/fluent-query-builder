namespace FluentQueryBuilder.Converters
{
    public class DoubleConverter: IPropertyConverter
    {
        public object Convert(string source, params object[] parameters)
        {
            return double.Parse(source);
        }

        public string ConvertBack(object source, params object[] parameters)
        {
            double value = (double)source;
            return value.ToString();
        }
    }
}
