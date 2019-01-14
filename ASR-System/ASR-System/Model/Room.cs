using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ASR_System.Model
{
    public class Room
    {
        public string RoomID { get; }

        public Room(string inputRoomID)
        {
            RoomID = inputRoomID;
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

        //DATA VALIDATION METHODS

        //REGEX IS ^A|B|C|D$
        public static bool RoomIdValidation(string roomId)
        {
            string regexString = @"^A|B|C|D$";
            return Regex.IsMatch(roomId, regexString);
        }
    }
}
