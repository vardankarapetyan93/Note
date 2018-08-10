using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Contacts
{
    public class User
    {
       
        [JsonIgnore]
        public Notepad notepad;
        public string Username { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Format { get; set; }
        public User(string name, string surname, string username, string password, string format)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            Format = format;
            notepad = new Notepad();
        }
       

        public override string ToString()
        {
            return $"{Name} {Surname} {Username} {Password} {Format}";
        }

    }
}
