﻿namespace FamiliesAPI.Entities.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HashKey { get; set; }
    }
}
