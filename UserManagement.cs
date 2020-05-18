using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Snake
{    
    class UserManagement
    {
        private List<User> userlist;
        private List<string> Sorted;
        public UserManagement() {
            userlist = new List<User>();
            Sorted = new List<string>();
        }

        //add user to the list
        public void AddUser(User user) {
            userlist.Add(user);
        }

        //display the ranking record from the text file when user clears the game
        public void readRecord()
        {
            var path = "userRecord.txt";
            using (StreamReader file = new StreamReader(path))
            {
                string ln;
                while ((ln=file.ReadLine()) != null)
                {
                    Console.WriteLine(ln);
                }
            }
        }

        //add the user record into the text file
        public void recordUser() {
            var path = "userRecord.txt";
            StreamWriter sw = File.AppendText(path);
            foreach (User user in userlist) {
                sw.WriteLine(user.getTime.ToString() + '\t' + user.getName + '\t' + user.getScore.ToString());
            }
            sw.Close();
        }

        //sort
        public void sortRecord() {
            var path = "userRecord.txt";
            using (StreamReader file = new StreamReader(path))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    Sorted.Add(ln);
                }
            }
            if (Sorted.Count > 0)
            {
                Sorted.Sort();
                System.IO.File.WriteAllText(path, string.Empty);
                StreamWriter sw = File.AppendText(path);
                foreach (string user in Sorted)
                {
                    sw.WriteLine(user);
                }
                sw.Close();
            }
        }

        public List<User> getUsers
        {
            get { return userlist; }
        }
    }
}
