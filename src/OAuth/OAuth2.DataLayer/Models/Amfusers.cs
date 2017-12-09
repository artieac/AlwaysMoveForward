﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class Amfusers
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordHint { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserStatus { get; set; }
        public int Role { get; set; }
        public string ResetToken { get; set; }
    }
}