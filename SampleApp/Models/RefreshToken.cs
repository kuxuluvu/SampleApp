using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public long Expiration { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public bool IsExpired()
        {
            try
            {
                var dateExpiration = new DateTime(Expiration);
                return dateExpiration >= DateTime.Now;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
