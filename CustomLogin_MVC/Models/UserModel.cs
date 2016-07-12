using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomLogin_MVC.Models
{
    public class UserModel
    {
        [Required]
       
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        //[StringLength(150)]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[StringLength(200), MinLength(6)]
        [Display(Name = "Password  ")]
        public string Password { get; set; }

        public System.Guid Id { get; set; }

        public string PasswordSalt { get; set; }
        public int ReplyCount { get; set; }
    }
}