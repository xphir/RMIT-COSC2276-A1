using ASR_System.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace ASR_System.Controller
{
    public static class MainEngine
    {
        //Print out a list of users
        public static void PrintUserList(List<User> userList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", "ID", "Name", "Email"));

            if (userList.Any())
            {
                foreach (User selectedUser in userList)
                {
                    Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", selectedUser.UserID, selectedUser.Name, selectedUser.Email));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        public static void PrintRoomList(List<Room> roomList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(String.Format("\t{0,-20}", "Room name"));

            //Check if any rooms currently exist
            if (roomList.Any())
            {
                foreach (Room selectedRoom in roomList)
                {
                    Console.WriteLine(String.Format("\t{0,-20}", selectedRoom.RoomID));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        public static void PrintSlotList(List<Slot> slotList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", "Room name", "Start time", "End time", "Staff ID", "Bookings"));
            if (slotList.Any())
            {
                foreach (Slot selectedSlot in slotList)
                {
                    Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", selectedSlot.RoomID, selectedSlot.StartTime.ToString("HH:mm"), selectedSlot.StartTime.AddHours(1).ToString("HH:mm"), selectedSlot.StaffID, selectedSlot.BookedInStudentID));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }


        private static void PrintSlots(List<Slot> slotList, DateTime date)
        {

            var slots = slotList.Where(x => x.StartTime.Date == date).ToList();

            if (!slots.Any())
            {
                Console.WriteLine("No slots found!");
                return;
            }

            Console.WriteLine("{0,-10}{1,-20}{2,-10}{3}",
                "Room ID", "Star Time", "Staff ID", "Student ID");

            foreach (var slot in slots)
            {
                Console.WriteLine("{0,-10}{1,-20:dd/MM/yyyy hh:mm}{2,-10}{3}",
                    slot.RoomID, slot.StartTime, slot.StaffID, slot.BookedInStudentID);
            }
        }

        //Get a list of slots on a given day
        public static List<Slot> GetDaySlots(List<Slot> slotList, DateTime requestedDate)
        {
            return slotList.Where(x => x.StartTime.Date == requestedDate.Date).ToList();
        }

        public static List<Slot> GetAvailibleSlots(List<Slot> slotList)
        {
            return slotList.Where(x => x.BookedInStudentID != null).ToList();
        }

        //Get a list of slots with bookings
        public static List<Slot> GetUnavailibleSlots(List<Slot> slotList)
        {
            return slotList.Where(x => x.BookedInStudentID == null).ToList();
        }


        //Get a list of availible rooms on a given date (used for staff menu)
        //BUSINESS RULE: Each room can be booked for a maximum of 2 slots per day.
        //BUSINESS RULE: A staff member can book a maximum of 4 slots per day
        public static List<Room> GetRoomAvailability(List<Slot> slotList, DateTime selectedDate)
        {
            var roomList = slotList.Where(x => x.StartTime.Date == selectedDate.Date).Where(x => x.BookedInStudentID == null).ToList();
            slotList.Where(x => x.BookedInStudentID == null).ToList();

            //Create return list
            var daySlotList = new List<Slot>();
            var availibleSlotList = new List<Slot>();
            var unavailibleSlotList = new List<Slot>();
            var returnRoomList = new List<Room>();

            //Get the slots on a given day
            daySlotList = GetDaySlots(slotList, selectedDate);
            //Get the list of unavailible slots on that day
            unavailibleSlotList = GetUnavailibleSlots(daySlotList);
            //Get the list of availible slots on that day
            availibleSlotList = GetAvailibleSlots(daySlotList);

            //Run through the availible slots
            foreach (Slot selectedSlot in availibleSlotList)
            {
                //check the ammount of times selected slot's room has been booked
                if (CountRoomBookings(unavailibleSlotList, selectedSlot.RoomID) < MiscellaneousUtilities.ROOM_BOOKING_LIMIT)
                {
                    //if its less than 2 add it to the list
                    returnRoomList.Add(new Room(selectedSlot.RoomID));
                }
            }

            //Return the results list
            return returnRoomList;
        }








        //Checks to see if UserID exists
        public static Boolean CheckIdExists(List<User> inputUserList, string userID)
        {
            foreach (User selectedUser in inputUserList)
            {
                if(selectedUser.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }





        //Count the ammount of room bookings in a list
        //BUSINESS RULE: Each room can be booked for a maximum of 2 slots per day.
        public static int CountRoomBookings(List<Slot> slotList, String roomID)
        {
            int count = 0;

            foreach (Slot selectedSlot in slotList)
            {
                if(selectedSlot.RoomID == roomID)
                {
                    count++;
                }
            }

            return count;
        }



        //Count the ammount of bookings in a given slotList by a student
        //BUSINESS RULE: A student can only make 1 booking per day
        public static int CountStudentBookings(List<Slot> slotList, String studentID)
        {
            int count = 0;

            foreach (Slot selectedSlot in slotList)
            {
                if (selectedSlot.BookedInStudentID == studentID)
                {
                    count++;
                }
            }

            return count;
        }

        //Count the ammount of bookings in a given slotList by a staff
        //BUSINESS RULE: A staff member can book a maximum of 4 slots per day
        public static int CountStaffBookings(List<Slot> slotList, String staffID)
        {
            int count = 0;

            foreach (Slot selectedSlot in slotList)
            {
                if (selectedSlot.StaffID == staffID)
                {
                    count++;
                }
            }

            return count;
        }



        //Get a list of availible booking slots for a student on a given day for a specific staffID
        public static List<Slot> GetStaffAvailability(List<Slot> slotList, DateTime selectedTime, String staffID)
        {
            var returnSlotList = new List<Slot>();
            var daySlotList = new List<Slot>();
            var availibleSlotList = new List<Slot>();
            var unavailibleSlotList = new List<Slot>();

            //Get the slots on a given day
            daySlotList = GetDaySlots(slotList, selectedTime);
            //Get the list of unavailible slots on that day
            unavailibleSlotList = GetUnavailibleSlots(daySlotList);
            //Get the list of availible slots on that day
            availibleSlotList = GetAvailibleSlots(daySlotList);

            foreach (Slot selectedSlot in availibleSlotList)
            {
                if(selectedSlot.StaffID == staffID)
                {
                    returnSlotList.Add(selectedSlot);
                }
            }


            //Return the results list
            return returnSlotList;

        }


        public static void CreateSlot()
        {
            var SlotList = new SlotManager();
            Slot newSlot;

            Console.WriteLine("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter date for slot(dd - mm - yyyy): ");
            string inputDate = Console.ReadLine();

            Console.WriteLine("Enter time for slot (hh:mm): ");
            string inputTime = Console.ReadLine();

            Console.WriteLine("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

            newSlot = ValidateSlot(inputRoom, inputDate, inputTime, inputStaffID);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                SlotList.CreateSlot(newSlot);
                Console.WriteLine("Slot created successfully."); ;
            }

        }

        public static Slot ValidateSlot(string inputRoom, string inputDate, string inputTime, string inputStaffID)
        {
            Slot returnSlot = null;
            DateTime dateOnly;
            DateTime? dateOnlyNullable;
            TimeSpan? timeOnlyNullable;
            TimeSpan timeOnly;
            DateTime combinedTime;

            //VALIDATE THE ROOM
            if (!(ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to create slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE
            dateOnlyNullable = ValidateDate(inputDate);
            if (!(dateOnlyNullable.HasValue))
            {
                Console.WriteLine("Unable to create slot: Invalid Date");
                return null;
            }
            else
            {
                //Cast nullable to normal
                dateOnly = (DateTime)dateOnlyNullable;
            }

            //VALIDATE THE TIME
            timeOnlyNullable = ValidateTime(inputTime);
            if (!(timeOnlyNullable.HasValue))
            {
                Console.WriteLine("Unable to create slot: Invalid Time");
                return null;
            }
            else
            {
                //Cast nullable to normal
                timeOnly = (TimeSpan)timeOnlyNullable;
            }

            //COMBINE THE DATE AND TIME
            try
            {
                combinedTime = dateOnly.Add(timeOnly);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to create slot: Invalid Date+Time Combination");
                return null;
            }

            //VALIDATE THE STAFF ID
            if (!(ValidateStaffId(inputStaffID)))
            {
                Console.WriteLine("Unable to create slot: Invalid StaffID");
                return null;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            returnSlot = new Slot(inputRoom, combinedTime, inputStaffID);
            
            return returnSlot;
        }

       
        public static Boolean ValidateRoom(string roomID)
        {
            if(Room.RoomIdValidation(roomID))
            {
                return true;
            }

            return false;
        }


        public static DateTime? ValidateDate(string inputDate)
        {
            //If inputDate passes the regex check and the TryParseExact return a DateTime else return null
            if ((Slot.dateRegexCheck(inputDate)) && (DateTime.TryParseExact(inputDate, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime dateOnly)))
            {
                return dateOnly;
            }

            return null;        
        }

        public static TimeSpan? ValidateTime(string inputTime)
        {
            //If inputTime passes the regex check and the TryParseExact return a TimeSpan else return null
            if ((Slot.timeRegexCheck(inputTime)) && (TimeSpan.TryParseExact(inputTime, "HH:mm", null, TimeSpanStyles.None, out TimeSpan timeOnly)))
            {
                return timeOnly;
            }

            return null;
        }


        public static Boolean ValidateStaffId(string staffID)
        {
            return new StaffManager().StaffList.Where(x => x.UserID == staffID).Any();
        }
    }
}
