﻿namespace webapi.Models
{
    public class User
    {
        public User(int id, string username, string? password, string? role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
