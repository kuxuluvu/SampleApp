namespace SampleApp.ViewModels
{
    public class TokenResponseModel
    {
        public bool IsSucces { get; set; }
        public string ErrorMessage { get; set; }
        public AccessTokenViewModel AccessToken { get; set; }
    }
}
