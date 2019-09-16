using System.Net;

namespace SampleApp.Services.DTOs
{
    public class ResponseUploadImageDto
    {
        public DataReponse Data { get; set; }
        public HttpStatusCode Status { get; set; }
    }
    public class DataReponse
    {
        public string Link { get; set; }
    }
}
