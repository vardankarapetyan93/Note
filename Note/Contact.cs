using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts
{
    public class Contact
    {
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Phone { get; set; }
        public List<string> Email { get; set; }


        public Contact()
        {

        }
        public Contact(string name, string surname, List<string> phone, List<string> email)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
        }
        public override string ToString()
        {
            return $"{Name} {Surname} {StringConcat(Phone)} {StringConcat(Email)}";
        }

        public string StringConcat(List<string> list)
        {
            string s = "";

            for (int i = 0; i < list.Count; i++)
            {
                s += list[i];
                if (i < list.Count - 1)
                {
                    s += ",";
                }
            }

            return s;
        }

    }
}
