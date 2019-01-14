using ASR_System.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace ASR_System.Controller
{
    public class MainEngine
    {
        public List<User> UserList { get; set; }
        public List<Staff> StaffList { get; set; }
        public List<Student> StudentList { get; set;  }
        public List<Room> RoomList { get; set; }
        public List<Slot> SlotList { get; set; }

        public void CreateData()
        {
            SlotList.Add(new Slot("A", DateTime.Now, "e12345"));
            SlotList.Add(new Slot("B", DateTime.Now, "e12345"));
            SlotList.Add(new Slot("C", DateTime.Now, "e12345"));
            SlotList.Add(new Slot("D", DateTime.Now, "e12345"));

            PrintSlotList(SlotList);
        }


        public void ListStaff()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from dbo.[User] WHERE UserID LIKE 'e%'";

                StaffList = command.GetDataTable().Select().Select(x => new Staff((string)x["UserID"], (string)x["Name"], (string)x["Email"])).ToList();
            }
        }

        public void ListStudents()
        {
            using (var connection = Program.ConnectionString.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from dbo.[User] WHERE UserID LIKE 's%'";

                StudentList = command.GetDataTable().Select().Select(x => new Student((string)x["UserID"], (string)x["Name"], (string)x["Email"])).ToList();
            }
        }


        //public void UpdateItemPrice(Staff item)
        //{
        //    using (var connection = Program.ConnectionString.CreateConnection())
        //    {
        //        connection.Open();

        //        var command = connection.CreateCommand();
        //        command.CommandText = "update Inventory set Price = @price where InventoryID = @inventoryID";
        //        command.Parameters.AddWithValue("price", item.Price);
        //        command.Parameters.AddWithValue("inventoryID", item.InventoryID);

        //        command.ExecuteNonQuery();
        //    }
        //}

        //Checks to see if UserID exists
        public Boolean CheckIdExists(List<User> inputUserList, string userID)
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


        //Print out a list of users
        public void PrintUserList(List<User> userList)
        {
            Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", "ID", "Name", "Email"));
            foreach (User selectedUser in userList)
            {
                Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", selectedUser.UserID, selectedUser.Name, selectedUser.Email));
            }
        }

        //Print out a list of rooms
        public void PrintRoomList(List<Room> roomList)
        {
            Console.WriteLine(String.Format("\t{0,-20}", "Room name"));
            foreach (Room selectedRoom in roomList)
            {
                Console.WriteLine(String.Format("\t{0,-20}", selectedRoom.RoomID));
            }
        }

        //Print out a list of slots
        public void PrintSlotList(List<Slot> slotList)
        {                         
            Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", "Room name", "Start time", "End time", "Staff ID", "Bookings"));
            foreach (Slot selectedSlot in slotList)
            {
                Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", selectedSlot.RoomID, selectedSlot.StartTime.ToString("HH:mm"), selectedSlot.StartTime.AddHours(1).ToString("HH:mm"), selectedSlot.StaffID, selectedSlot.BookedInStudentID));
            }
        }

        //Get a list of slots without bookings
        public List<Slot> GetAvailibleSlots(List<Slot> slotList)
        {
            //Create return list
            var availibleSlotList = new List<Slot>();

            //Run through each slot
            foreach (Slot selectedSlot in slotList)
            {
                //If the slot is availible
                if(selectedSlot.BookedInStudentID == "-")
                {
                    availibleSlotList.Add(selectedSlot);
                }
            }

            return availibleSlotList;
        }

        //Get a list of slots with bookings
        public List<Slot> GetUnavailibleSlots(List<Slot> slotList)
        {
            //Create return list
            var unavailibleSlotList = new List<Slot>();

            //Run through each slot
            foreach (Slot selectedSlot in slotList)
            {
                //If the slot is availible
                if (selectedSlot.BookedInStudentID != "-")
                {
                    unavailibleSlotList.Add(selectedSlot);
                }
            }
            return unavailibleSlotList;
        }

        //Count the ammount of room bookings in a list
        //BUSINESS RULE: Each room can be booked for a maximum of 2 slots per day.
        public int CountRoomBookings(List<Slot> slotList, String roomID)
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

        //Get a list of slots on a given day
        public List<Slot> GetDaySlots(List<Slot> slotList, DateTime requestedDate)
        {
            var daySlotList = new List<Slot>();

            foreach (Slot selectedSlot in slotList)
            {
                if(selectedSlot.StartTime.Date == requestedDate.Date)
                {
                    daySlotList.Add(selectedSlot);
                }
            }

            return daySlotList;
        }

        //Count the ammount of bookings in a given slotList by a student
        //BUSINESS RULE: A student can only make 1 booking per day
        public int CountStudentBookings(List<Slot> slotList, String studentID)
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
        public int CountStaffBookings(List<Slot> slotList, String staffID)
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

        //Get a list of availible rooms on a given date (used for staff menu)
        //BUSINESS RULE: Each room can be booked for a maximum of 2 slots per day.
        //BUSINESS RULE: A staff member can book a maximum of 4 slots per day
        public List<Room> GetRoomAvailability(List<Slot> slotList, DateTime selectedTime)
        {
            //Create return list
            var daySlotList = new List<Slot>();
            var availibleSlotList = new List<Slot>();
            var unavailibleSlotList = new List<Slot>();
            var returnRoomList = new List<Room>();

            //Get the slots on a given day
            daySlotList = GetDaySlots(slotList, selectedTime);
            //Get the list of unavailible slots on that day
            unavailibleSlotList = GetUnavailibleSlots(daySlotList);
            //Get the list of availible slots on that day
            availibleSlotList = GetAvailibleSlots(daySlotList);

            //Run through the availible slots
            foreach (Slot selectedSlot in availibleSlotList)
            {
                //check the ammount of times selected slot's room has been booked
                if(CountRoomBookings(unavailibleSlotList, selectedSlot.RoomID) < 2)
                {
                    //if its less than 2 add it to the list
                    returnRoomList.Add(new Room(selectedSlot.RoomID));
                }
            }

            //Return the results list
            return returnRoomList;
        }

        //Get a list of availible booking slots for a student on a given day for a specific staffID
        public List<Slot> GetStaffAvailability(List<Slot> slotList, DateTime selectedTime, String staffID)
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


        public void CreateSlot()
        {
            DateTime combinedTime;
            DateTime dateOnly;
            TimeSpan timeOnly;

            Console.WriteLine("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper(new CultureInfo("en-US", false));

            Console.WriteLine("Enter date for slot(dd - mm - yyyy): ");
            string inputDate = Console.ReadLine();

            Console.WriteLine("Enter time for slot (hh:mm): ");
            string inputTime = Console.ReadLine();

            Console.WriteLine("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

            //VALIDATE THE ROOM
            if (ValidateRoom(inputRoom))
            {
                Console.WriteLine("Unable to create slot.");
                return;
            }

            //VALIDATE THE DATE
            try
            {
                dateOnly = ValidateDate(inputDate);
            }
            catch(Exception)
            {
                Console.WriteLine("Unable to create slot.");
                return;
            }

            //VALIDATE THE TIME
            try
            {
                timeOnly = ValidateTime(inputTime);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to create slot.");
                return;
            }

            //COMBINE THE DATE AND TIME
            try
            {
                combinedTime = dateOnly.Add(timeOnly);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to create slot.");
                return;
            }

            //VALIDATE THE STAFF ID
            if (ValidateStaffId(inputStaffID))
            {
                Console.WriteLine("Unable to create slot.");
                return;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            SlotList.Add(new Slot(inputRoom, combinedTime, inputStaffID));
            Console.WriteLine("Slot created successfully."); 

        }

       
        public static Boolean ValidateRoom(string roomID)
        {
            if(DataValidation.roomIdValidation(roomID))
            {
                return true;
            }

            return false;
        }


        public static DateTime ValidateDate(string inputDate)
        {           
            if(!DataValidation.dateRegexCheck(inputDate))
            {
                throw new Exception("Invalid Date Entered: Please enter a valid date format (dd-mm-yyyy)");
            }
            //DateTime.TryParseExact(inputDate, "dd-MM-yyyy",null, DateTimeStyles.AdjustToUniversal, out DateTime dateOnly);
            if(!DateTime.TryParse(inputDate, out DateTime dateOnly))
            {
                throw new Exception("Date Parse Failed");
            }

            return dateOnly;
        }

        public static TimeSpan ValidateTime(string inputTime)
        {
            TimeSpan startTime = new TimeSpan(9, 0, 0); // 9:00am
            TimeSpan endTime = new TimeSpan(14, 0, 0); // 2:00pm

            if (!DataValidation.timeRegexCheck(inputTime))
            {
                throw new Exception("Invalid Time Entered: Please enter a valid time format (hh:00)");
            }
            
            if(!TimeSpan.TryParse(inputTime, out TimeSpan timeOnly))
            {
                throw new Exception("Time Parse Failed");
            }

            if ((timeOnly >= startTime) && (timeOnly <= endTime))
            {
                return timeOnly;
            }
            else
            {
                throw new Exception("Invalid Time Entered: Please enter a time between 09:00 and 14:00");
            }
        }

        public Boolean ValidateStaffId(string staffID)
        {
            foreach (Staff selectedUser in StaffList)
            {
                if (selectedUser.UserID == staffID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
