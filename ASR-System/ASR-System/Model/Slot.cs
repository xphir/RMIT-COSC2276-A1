using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ASR_System.Model
{
    public class Slot
    {
        public string RoomID { get; }
        public DateTime StartTime { get; }
        public string StaffID { get; }
        public string BookedInStudentID { get; set; }

        public Slot(string roomID, DateTime startTime, string staffID)
        {
            RoomID = roomID;
            StartTime = startTime;
            StaffID = staffID;
            BookedInStudentID = null;
        }

        public Slot(string roomID, DateTime startTime, string staffID, string bookedInStudentID)
        {
            RoomID = roomID;
            StartTime = startTime;
            StaffID = staffID;
            BookedInStudentID = bookedInStudentID;
        }

        public Boolean makeBooking(string studentID)
        {
            if(BookedInStudentID == null)
            {
                //Slot Can be booked
                BookedInStudentID = studentID;
                return true;
            }
            else
            {
                Console.WriteLine("Error: Selected slot already has a booking");
                return false;
            }
        }

        public int CountStaffBooking(string staffID)
        {
            if(User.StaffIdValidation(staffID))
            {
                //StaffID is valid
            }
            else
            {
                //StaffID is not valid
            }
            return 0;
        }


        //a) Each slot must be of 1-hour duration.
        //b) A staff member can book a maximum of 4 slots per day.
        //c) The slots must be booked between the school working hours of 9am to 2pm and will always be booked at the start of the hour, e.g., 10:00am, 1:00pm, etc…
        //d) Each room can be booked for a maximum of 2 slots per day.
        //e) A staff member cannot delete a slot once it has been booked by a student.
        //f) A student can only make 1 booking per day.
        //g) A slot can have a maximum of 1 student booked into it.

        //DATA VALIDATION METHODS

        // HH:MM REGEX ^([0-1][0-9]|2[0-3]):[0-5][0-9]$
        // HH:00 REGEX ^([0-1][0-9]|2[0-3]):00$
        public static bool timeRegexCheck(string inputTime)
        {
            string timeRegexString = @"^([0-1][0-9]|2[0-3]):00$";
            return Regex.IsMatch(inputTime, timeRegexString);
        }

        //dd-mm-yyyy REGEX ^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$
        public static bool dateRegexCheck(string inputDate)
        {
            string dateRegexString = @"^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$";
            return Regex.IsMatch(inputDate, dateRegexString);
        }
    }
}
