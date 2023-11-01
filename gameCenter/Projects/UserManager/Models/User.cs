using System;

namespace WpfApp1.Models
{
    public class User
    {
        public static int count = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Created = DateTime.Now;
            count++;
            Id = count;
        }
    }
}
