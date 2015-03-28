using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPi.Portal.Models
{
    public class CreateAccountViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MinLength(4)]
        [Required]
        public string Password { get; set; }

        [MinLength(4)]
        [Compare("Password")]
        [Required]
        public string PasswordConfirm { get; set; }
    }
}