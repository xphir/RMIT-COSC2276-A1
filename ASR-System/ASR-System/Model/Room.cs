using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Model
{
    public class Room
    {
        public string RoomID { get; }

        public Room(string roomID)
        {
            RoomID = roomID;
        }
    }
}
