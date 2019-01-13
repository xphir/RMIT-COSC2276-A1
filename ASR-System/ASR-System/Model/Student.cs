using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Model
{
    class Student : User
    {
        public Student(string userID, string name, string email) : base(userID, name, email) { }
    }

}
