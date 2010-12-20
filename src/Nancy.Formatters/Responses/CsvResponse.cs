namespace Nancy.Formatters.Responses
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class CsvResponse<TModel> : Response
    {
        public CsvResponse(TModel model)
        {
            this.Contents = GetCsvContents(model);
            this.ContentType = "text/csv";
        }

        private static Action<Stream> GetCsvContents(TModel model)
        {
            return stream =>
            {
                var sw = new StreamWriter(stream);

                if (model != null)
                {
                    var properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
                    sw.WriteLine(String.Join(",", properties.Select(x => x.Name)));
                    sw.Write(String.Join(",", properties.Select(x => x.GetValue(model, null))));
                }
                else
                {
                    sw.Write("null");
                }

                sw.Flush();
            };
        }
    }
}