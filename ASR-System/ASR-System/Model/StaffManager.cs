﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASR_System.Model
{
    class StaffManager
    {
        public List<Staff> StaffList { get; }

        public StaffManager()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from Slot where UserID like 'e%";

                StaffList = command.GetDataTable().Select().Select(x =>
                    new Staff((string)x["UserID"], (string)x["Name"], (string)x["Email"])).ToList();
            }
        }
    }
}
