using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class UserParameterModel
    {
        public UserParameterModel()
        {
            Page = 1;
            PageSize = 10;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string ColumnSort { get; set; }
        public string Search { get; set; }
        
    }
}
