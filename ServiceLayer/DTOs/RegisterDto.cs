using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class RegisterDto
    {
        [Required (ErrorMessage = "user name is required")]
        public string UserName { get; set; }
        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
