using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class RefreshTokenViewModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}
