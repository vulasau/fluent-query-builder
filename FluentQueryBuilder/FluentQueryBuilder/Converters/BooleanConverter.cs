namespace FluentQueryBuilder.Converters
{
    public class BooleanConverter: IPropertyConverter
    {
        public object Convert(string source)
        {
            return bool.Parse(source);
        }

        public string ConvertBack(object source)
        {
            bool value = (bool) source;
            return value.ToString();
        }
    }
}
