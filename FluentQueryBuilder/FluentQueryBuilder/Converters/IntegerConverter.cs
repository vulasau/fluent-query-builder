namespace FluentQueryBuilder.Converters
{
    public class IntegerConverter : IPropertyConverter
    {
        public object Convert(string source, params object[] parameters)
        {
            return int.Parse(source);
        }

        public string ConvertBack(object source, params object[] parameters)
        {
            int value = (int)source;
            return value.ToString();
        }
    }
}
