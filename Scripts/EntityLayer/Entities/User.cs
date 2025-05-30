﻿using System;
namespace EntityLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? NameSurname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; } = "STUDENT";
        public bool IsActive { get; set; } = false;
    }
}

