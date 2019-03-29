using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace sys.Application.Api
{
    public class UserModel
    {
        [Required]

        [Display(Name = "User Name")] 
        public string UserName { get; set; }

        [Required]

        [DataType(DataType.Password)]

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]

        public string Password { get; set; }
    }
}