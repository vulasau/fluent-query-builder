namespace FluentQueryBuilder.Converters
{
    public class IntegerConverter : IPropertyConverter
    {
        public object Convert(string source)
        {
            return int.Parse(source);
        }

        public string ConvertBack(object source)
        {
            int value = (int)source;
            return value.ToString();
        }
    }
}
