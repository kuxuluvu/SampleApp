using SampleApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class TokenResponseModel
    {
        public bool IsSucces { get; set; }
        public string ErrorMessage { get; set; }
        public AccessTokenViewModel AccessToken { get; set; }
    }
}
