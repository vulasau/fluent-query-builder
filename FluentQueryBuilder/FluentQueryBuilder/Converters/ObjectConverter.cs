namespace FluentQueryBuilder.Converters
{
    public class ObjectConverter: IPropertyConverter
    {
        public object Convert(string source, params object[] parameters)
        {
            return source;
        }

        public string ConvertBack(object source, params object[] parameters)
        {
            return source != null ? source.ToString() : null;
        }
    }
}
