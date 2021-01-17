using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "user name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }
}
