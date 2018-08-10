using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Contacts
{
    public class MyConsole
    {


        private User user;
        private List<User> userList;


        public void Start()
        {
            DeserializeUser();
            RegOrLogin();
            Menu();

        }
        private void RegOrLogin()
        {
            ConsoleKey key;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome");
                Console.WriteLine();
                Console.WriteLine("Registration : 1        Login : 2");
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad1)
                {
                    Registration();
                    Login();
                    break;
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    Login();
                    break;
                }
            }

        }
        private void Menu()
        {
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                ShowNotepad();
                if (user.notepad.contactList.Count == 0)
                {
                    Console.WriteLine("Add : 1");
                }
                else
                {
                    Console.WriteLine("Add contact : 1        Choose contact : 2        Exit : Esc");
                }

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad1)
                {
                    AddContact();
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    ChooseContact();
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }

            }
        }
        private void Login()
        {
            string username;
            string password;


            Console.Clear();
            Console.WriteLine("Enter username...");
            username = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter password...");
            password = Console.ReadLine();
            Console.Clear();

            if (CheckPassword(username, password))
            {

                if (user.Format == "True")
                {
                    user.notepad.DeserializeContactsJson(user);
                }
                else if (user.Format == "False")
                {
                    user.notepad.DeserializeContactsXml(user);
                }

            }
            else
            {
                Console.WriteLine("Incorrect username or password!");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                RegOrLogin();
            }

        }
        private void Registration()
        {
            bool b;
            string name;
            string surname;
            string username;
            string password;
            string repPass;
            Console.Clear();
            Console.WriteLine("Enter your name...");
            Console.WriteLine();
            name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter your surname...");
            Console.WriteLine();
            surname = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter your username...");
            Console.WriteLine();
            username = Console.ReadLine();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter your password...");
                Console.WriteLine();
                password = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Repeat your password...");
                Console.WriteLine();
                repPass = Console.ReadLine();
                Console.Clear();

                if (password == repPass)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Password does not match. Please re-enter a new password again.");
                    Console.ReadKey();
                }
            }
            b = JsonOrXml();
            userList.Add(new User(name, surname, username, password, b.ToString()));
            user = new User(name, surname, username, password, b.ToString());

            if (b == true)
            {
                using (new FileStream($"{user.Username}.json", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {

                }

            }
            else if (b == false)
            {
                using (new FileStream($"{user.Username}.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {

                }
            }
            SerializeUser();
        }

        private void SerializeUser()
        {
            using (StreamWriter sW = new StreamWriter("user.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sW, userList);
            }
        }
        private void DeserializeUser()
        {
            FileStream f = new FileStream("user.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            if (f.Length == 0)
            {
                userList = new List<User>();
                f.Close();
            }
            else
            {
                f.Close();
                userList = new List<User>();
                string json = File.ReadAllText(f.Name);
                userList = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }
        private bool JsonOrXml()
        {
            ConsoleKey key;
            bool b = false;
            Console.WriteLine("1 : Json     2 : XML");
            key = Console.ReadKey().Key;
            while (true)
            {
                if (key == ConsoleKey.NumPad1)
                {
                    b = true;
                    break;
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        private bool CheckPassword(string username, string password)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (password == userList[i].Password && username == userList[i].Username)
                {
                    user = userList[i];
                    return true;
                }
            }
            return false;
        }
        private void AddContact()
        {
            Console.Clear();
            string name;
            string surname;
            Console.WriteLine("Enter contact name...");
            name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter contact surname...");
            surname = Console.ReadLine();
            Console.Clear();
            List<string> phone = CreatePhone();
            List<string> email = CreateEmail();
            user.notepad.AddContac(name, surname, phone, email);

            SerializeContacts();

        }

        private List<string> CreatePhone()
        {
            List<string> phoneList = new List<string>();
            string s;
            ConsoleKey key;

            Console.WriteLine("Enter phone number...");
            s = Console.ReadLine();
            phoneList.Add(s);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add : 1     Continue : 2");
                key = Console.ReadKey().Key;
                Console.Clear();
                if (key == ConsoleKey.NumPad1)
                {
                    Console.WriteLine("Enter phone number...");
                    s = Console.ReadLine();
                    phoneList.Add(s);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    break;
                }
            }

            return phoneList;
        }

        private List<string> CreateEmail()
        {
            List<string> emailList = new List<string>();
            string s;
            ConsoleKey key;

            Console.WriteLine("Enter e-mail...");
            s = Console.ReadLine();
            emailList.Add(s);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add : 1     Continue : 2");
                key = Console.ReadKey().Key;
                Console.Clear();
                if (key == ConsoleKey.NumPad1)
                {

                    Console.WriteLine("Enter e-mail...");
                    s = Console.ReadLine();
                    emailList.Add(s);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    break;
                }
            }

            return emailList;
        }

        private void ShowNotepad()
        {
            Console.WriteLine($"{user.Name} {user.Surname}");
            Console.WriteLine();

            for (int i = 0; i < user.notepad.contactList.Count; i++)
            {
                Console.WriteLine($"{i + 1} : {user.notepad.contactList[i]}");
            }
            Console.WriteLine();
        }

        private void ChooseContact()
        {
            int position;
            bool b;
            Console.Clear();
            bool next = false;

            ShowNotepad();
            Console.WriteLine("Enter contact position...");
            Console.WriteLine();
            b = int.TryParse(Console.ReadLine(), out position);
            Console.Clear();
            if (b && (position > 0 && position <= user.notepad.contactList.Count))
            {
                next = true;
            }
            else
            {
                Console.WriteLine("Incorrect input!");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            if (next)
            {
                ChoosedContactView(position - 1);
            }

        }

        private void ChoosedContactView(int position)
        {
            ConsoleKey key;
            bool loopBreak;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(user.notepad.contactList[position]);
                Console.WriteLine();
                Console.WriteLine("Remove : 1        Edit : 2        Back : Esc");
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad1)
                {
                    RemoveContact(position, out loopBreak);
                    if (loopBreak == true)
                    {
                        break;
                    }
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    EditContact(position);
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private void RemoveContact(int position, out bool loopBreak)
        {
            loopBreak = false;
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(user.notepad.contactList[position]);
                Console.WriteLine();
                Console.WriteLine("Accept : Enter        Cancel : Esc");
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.Enter)
                {
                    user.notepad.contactList.RemoveAt(position);
                    SerializeContacts();
                    loopBreak = true;
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    loopBreak = false;
                    break;
                }
            }
        }

        private void EditContact(int position)
        {
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Edit contact");
                Console.WriteLine();
                Console.WriteLine(user.notepad.contactList[position]);
                Console.WriteLine();
                Console.WriteLine("Name : 1        Surname : 2        Phone : 3        E-mail : 4        Back : Esc");
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.NumPad1)
                {
                    EditName(position);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    EditSurname(position);
                }
                else if (key == ConsoleKey.NumPad3)
                {
                    EditPhone(position);
                }
                else if (key == ConsoleKey.NumPad4)
                {
                    EditEmail(position);
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }


            }


        }

        private void EditName(int position)
        {
            string tempName = user.notepad.contactList[position].Name;
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter new name...");
                Console.WriteLine();
                user.notepad.contactList[position].Name = Console.ReadLine();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(user.notepad.contactList[position]);
                    Console.WriteLine();
                    Console.WriteLine("Accept : Enter        Cancel : Esc");
                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        SerializeContacts();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        user.notepad.contactList[position].Name = tempName;
                        break;
                    }
                }
                break;
            }
        }
        private void EditSurname(int position)
        {
            string tempSurname = user.notepad.contactList[position].Surname;
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter new surname...");
                Console.WriteLine();
                user.notepad.contactList[position].Surname = Console.ReadLine();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(user.notepad.contactList[position]);
                    Console.WriteLine();
                    Console.WriteLine("Accept : Enter        Cancel : Esc");
                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        SerializeContacts();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        user.notepad.contactList[position].Surname = tempSurname;
                        break;
                    }
                }
                break;
            }
        }
        private void EditPhone(int position)
        {
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(user.notepad.contactList[position]);
                Console.WriteLine();
                Console.WriteLine("Add number : 1        Remove number : 2        Back : Esc");
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad1)
                {
                    AddPhone(position);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    RemoveNumber(position);
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }

            }

        }
        private void EditEmail(int position)
        {
            ConsoleKey key;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(user.notepad.contactList[position]);
                Console.WriteLine();
                Console.WriteLine("Add e-mail : 1        Remove e-mail : 2        Back : Esc");
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad1)
                {
                    AddEmail(position);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    RemoveEmail(position);
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }

            }

        }
        private void AddPhone(int position)
        {

            string s;
            ConsoleKey key;
            int phoneCount = user.notepad.contactList[position].Phone.Count;


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter new number...");
                Console.WriteLine();
                s = Console.ReadLine();
                user.notepad.contactList[position].Phone.Add(s);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(user.notepad.contactList[position]);
                    Console.WriteLine();
                    Console.WriteLine("Accept : Enter        Cancel : Esc");
                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        SerializeContacts();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        user.notepad.contactList[position].Phone.RemoveAt(phoneCount);
                        break;
                    }
                }
                break;
            }
        }
        private void AddEmail(int position)
        {

            string s;
            ConsoleKey key;
            int emailCount = user.notepad.contactList[position].Email.Count;


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter new e-mail...");
                Console.WriteLine();
                s = Console.ReadLine();
                user.notepad.contactList[position].Email.Add(s);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(user.notepad.contactList[position]);
                    Console.WriteLine();
                    Console.WriteLine("Accept : Enter        Cancel : Esc");
                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        SerializeContacts();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        user.notepad.contactList[position].Email.RemoveAt(emailCount);
                        break;
                    }
                }
                break;
            }
        }
        private void RemoveNumber(int position)
        {
            ConsoleKey key;
            int input;
            int phoneCount = user.notepad.contactList[position].Phone.Count;
            string tempNumber;


            bool b;
            Console.Clear();
            Console.WriteLine("Enter number position...");
            Console.WriteLine();
            Console.WriteLine(user.notepad.contactList[position]);
            Console.WriteLine();
            b = int.TryParse(Console.ReadLine(), out input);

            if (b && (input > 0 && input <= phoneCount))
            {
                tempNumber = user.notepad.contactList[position].Phone[input - 1];
                user.notepad.contactList[position].Phone.RemoveAt(input - 1);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(user.notepad.contactList[position]);
                    Console.WriteLine();
                    Console.WriteLine("Accept : Enter        Cancel : Esc");
                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        SerializeContacts();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        user.notepad.contactList[position].Phone.Insert(input - 1, tempNumber);
                        break;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect position!");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }
        private void RemoveEmail(int position)
        {
            ConsoleKey key;
            int input;
            int emailCount = user.notepad.contactList[position].Email.Count;
            string tempEmail;


            bool b;
            Console.Clear();
            Console.WriteLine("Enter e-mail position...");
            Console.WriteLine();
            Console.WriteLine(user.notepad.contactList[position]);
            Console.WriteLine();
            b = int.TryParse(Console.ReadLine(), out input);

            if (b && (input > 0 && input <= emailCount))
            {
                tempEmail = user.notepad.contactList[position].Email[input - 1];
                user.notepad.contactList[position].Email.RemoveAt(input - 1);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(user.notepad.contactList[position]);
                    Console.WriteLine();
                    Console.WriteLine("Accept : Enter        Cancel : Esc");
                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        SerializeContacts();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        user.notepad.contactList[position].Email.Insert(input - 1, tempEmail);
                        break;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect position!");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }
        private void SerializeContacts()
        {
            if (user.Format == "True")
            {
                user.notepad.SerializeContactsJson(user);
            }
            else if (user.Format == "False")
            {
                user.notepad.SerializeContactsXml(user);
            }
        }
    }

}
