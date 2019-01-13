using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Model
{
    public abstract class User
    {
        public string UserID { get; }
        public string Name { get; set; }
        public string Email { get; set; }

        protected User(string userID, string name, string email)
        {
            UserID = userID;
            Name = name;
            Email = email;
        }
    }
}
