﻿using System.ComponentModel.DataAnnotations;

namespace Store.Authorization.API.Models.Request
{
    public class UserRegistrationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CPF { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmationPassword { get; set; }
    }

    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
