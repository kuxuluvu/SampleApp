using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class AccessTokenViewModel
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
        public RefreshTokenViewModel RefreshToken { get; private set; }

        public AccessTokenViewModel(string token, long expiration, RefreshTokenViewModel refreshToken)
        {
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
        }
    }
}
