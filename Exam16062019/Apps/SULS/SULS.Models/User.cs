﻿namespace SULS.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}