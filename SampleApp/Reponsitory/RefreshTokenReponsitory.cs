using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Reponsitory
{
    public class RefreshTokenReponsitory : BaseReponsitory<RefreshToken>, IRefreshTokenReponsitory
    {
        public RefreshTokenReponsitory(SampleContext context) : base(context)
        {
        }
    }
}
