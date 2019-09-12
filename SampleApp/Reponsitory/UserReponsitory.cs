using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Reponsitory
{
    public class UserReponsitory : BaseReponsitory<User>, IUserReponsitory
    {
        public UserReponsitory(SampleContext context) : base(context)
        {
        }
    }
}
