﻿namespace StudentRegistration.Models.Login
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Store hashed passwords!
        public string Role { get; set; }
    }
}