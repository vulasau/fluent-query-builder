namespace FluentQueryBuilder.Converters
{
    public class ObjectConverter: IPropertyConverter
    {
        public object Convert(string source)
        {
            return source;
        }

        public string ConvertBack(object source)
        {
            return source != null ? source.ToString() : null;
        }
    }
}
