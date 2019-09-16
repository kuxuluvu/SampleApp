using SampleApp.Infrastructure;
using SampleApp.Infrastructure.Models;
using SampleApp.Reponsitory.Intefaces;

namespace SampleApp.Reponsitory
{
    public class UserReponsitory : BaseReponsitory<User>, IUserReponsitory
    {
        public UserReponsitory(SampleContext context) : base(context)
        {
        }
    }
}
