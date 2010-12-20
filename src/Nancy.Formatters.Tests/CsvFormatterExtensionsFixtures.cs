namespace Nancy.Formatters.Tests
{
    using System.IO;
    using System.Net;
    using FakeItEasy;
    using Nancy.Formatters.Tests.Fakes;
    using Xunit;
    using Nancy.Tests;

    public class CsvFormatterExtensionsFixtures
    {
        private readonly IResponseFormatter formatter;
        private readonly Person model;
        private readonly Response response;

        public CsvFormatterExtensionsFixtures()
        {
            this.formatter = A.Fake<IResponseFormatter>();
            this.model = new Person { FirstName = "Emmanuel", LastName = "Morales" };
            this.response = this.formatter.AsCsv(model);
        }

        [Fact]
        public void Should_return_a_response_with_the_standard_csv_content_type()
        {
            response.ContentType.ShouldEqual("text/csv");
        }

        [Fact]
        public void Should_return_a_response_with_status_code_200_OK()
        {
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Should_return_a_valid_model_in_csv_format()
        {
            using (var stream = new MemoryStream())
            {
                response.Contents(stream);
                stream.ShouldEqual("FirstName,LastName\r\nEmmanuel,Morales");
            }
        }

        [Fact]
        public void Should_return_null_in_csv_format()
        {
            var nullResponse = formatter.AsCsv<Person>(null);
            using (var stream = new MemoryStream())
            {
                nullResponse.Contents(stream);
                stream.ShouldEqual("null");
            }
        }
    }
}