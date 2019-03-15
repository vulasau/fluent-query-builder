namespace FluentQueryBuilder.Converters
{
    public class BooleanConverter: IPropertyConverter
    {
        public object Convert(string source, params object[] parameters)
        {
            return bool.Parse(source);
        }

        public string ConvertBack(object source, params object[] parameters)
        {
            bool value = (bool) source;
            return value.ToString();
        }
    }
}
