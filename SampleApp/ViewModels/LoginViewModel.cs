using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class  ResponseModel
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
