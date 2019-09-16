using SampleApp.Infrastructure;
using SampleApp.Infrastructure.Models;
using SampleApp.Reponsitory.Intefaces;

namespace SampleApp.Reponsitory
{
    public class RefreshTokenReponsitory : BaseReponsitory<RefreshToken>, IRefreshTokenReponsitory
    {
        public RefreshTokenReponsitory(SampleContext context) : base(context)
        {
        }
    }
}
