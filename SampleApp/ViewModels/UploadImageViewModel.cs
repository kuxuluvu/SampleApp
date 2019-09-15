using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApp.ViewModels
{
    public class UploadImageViewModel
    {
        [Required]
        public Guid UserId { get; set;}
        [Required]
        public IFormFile File { get; set; }
    }
}
