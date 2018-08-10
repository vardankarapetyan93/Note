using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Contacts
{
    public class Notepad
    {
        public List<Contact> contactList;
        public Notepad()
        {

        }

      
        public void DeserializeContactsJson(User u)
        {

            using (FileStream f = new FileStream($"{u.Username}.json", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (f.Length == 0)
                {
                    contactList = new List<Contact>();
                }
                else
                {
                    f.Close();
                    string json = File.ReadAllText(f.Name);
                    contactList = JsonConvert.DeserializeObject<List<Contact>>(json);
                }
            }
        }
        public void DeserializeContactsXml(User u)
        {
            using (FileStream f = new FileStream($"{u.Username}.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (f.Length == 0)
                {
                    contactList = new List<Contact>();
                }
                else
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Contact>));
                    contactList = (List<Contact>)xml.Deserialize(f);
                }
            }
        }
        public void AddContac(string name, string surname, List<string> phone, List<string> email)
        {
            Contact current = new Contact(name, surname, phone, email);
            contactList.Add(current);


        }
        public void SerializeContactsJson(User u)
        {

            using (StreamWriter sW = new StreamWriter($"{u.Username}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sW, contactList);
            }
        }
        public void SerializeContactsXml(User u)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Contact>));
            using (FileStream stream = File.OpenWrite($"{u.Username}.xml"))
            {
                serializer.Serialize(stream, contactList);
            }
        }
    }
}
