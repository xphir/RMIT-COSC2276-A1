using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Model
{
    abstract class User
    {
        private string UserID { get; }
        private string Name { get; set; }
        private string Email { get; set; }

        protected User(string userID, string name, string email)
        {
            UserID = userID;
            Name = name;
            Email = email;
        }
    }
}
