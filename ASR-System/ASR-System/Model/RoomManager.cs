using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ASR_System.Model
{
    class RoomManager
    {
        public List<Room> Rooms { get; }

        public RoomManager()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from Room";

                Rooms = command.GetDataTable().Select().Select(x =>
                    new Room((string)x["RoomID"])).ToList();
            }
        }
    }
}
