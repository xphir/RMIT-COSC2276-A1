using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Model
{
    public class Staff : User
    {
        public Staff(string userID, string name, string email) : base(userID, name, email) { }
    }

}
