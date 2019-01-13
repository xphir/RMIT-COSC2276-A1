using ASR_System.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Controller
{
    public class MainEngine
    {
        //public List<Staff> StaffList { get; }


        public void ListStaff()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from dbo.[User] WHERE UserID LIKE 'e%'";

                //StaffList = command.GetDataTable().Select().Select(x => new Inventory((int)x["InventoryID"], (string)x["Name"], (decimal)x["Price"])).ToList();
            }
        }
    }
}
