using System;

namespace SampleApp.Services.DTOs
{
    public class AccessTokenDto
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
        public RefreshTokenDto RefreshToken { get; private set; }

        public AccessTokenDto(string token, long expiration, RefreshTokenDto refreshToken)
        {
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
        }
    }
}
