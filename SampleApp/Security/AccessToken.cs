using SampleApp.Models;
using System;

namespace SampleApp.Security
{
    public class AccessToken 
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
        public RefreshToken RefreshToken { get; private set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken)
        {
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
        }
    }

}
