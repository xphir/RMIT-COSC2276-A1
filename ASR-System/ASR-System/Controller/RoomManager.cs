using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASR_System.Model;

namespace ASR_System.Controller
{
    class RoomManager
    {
        public List<Room> RoomList { get; }

        public RoomManager()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from Room";

                RoomList = command.GetDataTable().Select().Select(x =>
                    new Room((string)x["RoomID"])).ToList();
            }
        }
    }
}
