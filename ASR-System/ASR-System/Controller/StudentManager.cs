﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASR_System.Model;

namespace ASR_System.Controller
{
    class StudentManager
    {
        public List<Student> StudentList { get; }

        public StudentManager()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from [User] where UserID like 's%'";

                StudentList = command.GetDataTable().Select().Select(x =>
                    new Student((string)x["UserID"], (string)x["Name"], (string)x["Email"])).ToList();
            }
        }
    }
}
