namespace Nancy.Formatters
{
    using Responses;

    public static class CsvFormatterExtensions
    {
        public static Response AsCsv<TModel>(this IResponseFormatter formatter, TModel model)
        {
            return new CsvResponse<TModel>(model);
        }
    }
}
