namespace SampleApp.Services.DTOs
{
    public class TokenResponseDto
    {
        public bool IsSucces { get; set; }
        public string ErrorMessage { get; set; }
        public AccessTokenDto AccessToken { get; set; }
    }
}
