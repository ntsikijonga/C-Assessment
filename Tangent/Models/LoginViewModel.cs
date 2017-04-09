using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tangent.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string username
        { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password
        {
            get; set;
        }
    }
}

