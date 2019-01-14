using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.Model
{
    public class Room : IEquatable<Room>
    {
        public string RoomID { get; }

        public Room(string inputRoomID)
        {
            RoomID = inputRoomID;
        }

        public override bool Equals(object obj)
        {
            if (obj is Room)
                return Equals((Room)obj);
            return false;
        }

        //Equals override
        public bool Equals(Room room)
        {
            if (RoomID == room.RoomID)
            {
                return true;
            }
            return false;
        }
    }
}
