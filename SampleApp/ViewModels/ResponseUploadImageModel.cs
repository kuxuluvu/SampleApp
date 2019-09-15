using System.Net;

namespace SampleApp.ViewModels
{
    public class ResponseUploadImageModel
    {
        public DataReponse Data { get; set; }
        public HttpStatusCode Status { get; set; }
    }
    public class DataReponse
    {
        public string Link { get; set; }
    }
}
