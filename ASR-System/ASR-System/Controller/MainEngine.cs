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
        //LOGIC METHODS START

        public static bool ValidateStaffId(string staffID)
        {
            return new StaffManager().StaffList.Where(x => x.UserID == staffID).Any();
        }

        public static bool ValidateStudentID(string studentID)
        {
            return new StudentManager().StudentList.Where(x => x.UserID == studentID).Any();
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

        //Get a specifc unique slot
        public static Slot GetSlot(List<Slot> slotList, string roomID, DateTime startTime)
        {
            var resultSlotList = from Slot slot in slotList
                             where slot.RoomID == roomID && slot.StartTime == startTime
                             select slot;

            if (resultSlotList.Count() < MiscellaneousUtilities.ROOM_DOUBLEBOOKING_CHECK)
            {
                return null;
            }
            else if (resultSlotList.Count() > MiscellaneousUtilities.ROOM_DOUBLEBOOKING_CHECK)
            {
                return null;
            }
            else
            {
                return resultSlotList.FirstOrDefault();
            }
        }

        //Get a list of availible rooms on a given date (used for staff menu)
        //BUSINESS RULE: Each room can be booked for a maximum of 2 slots per day.
        //BUSINESS RULE: A staff member can book a maximum of 4 slots per day
        public static List<Room> GetRoomAvailability(List<Slot> slotList, DateTime selectedDate)
        {
            var roomList = slotList.Where(x => x.StartTime.Date == selectedDate.Date).Where(x => x.BookedInStudentID == null).ToList();

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
                if (CountRoomBookings(unavailibleSlotList, selectedSlot.RoomID) < MiscellaneousUtilities.ROOM_DAILY_BOOKING_LIMIT)
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


        public static void CheckRoomAvailability()
        {
            DateTime dateOnly;
            DateTime? dateOnlyNullable;
            List<Slot> slotList = new SlotManager().SlotList;

            Console.WriteLine("---Room availability---");
            Console.Write("Enter date for room availability (dd-mm-yyyy): ");

            string inputDate = Console.ReadLine();

            //VALIDATE THE DATE
            dateOnlyNullable = Slot.ValidateDate(inputDate);
            if (!(dateOnlyNullable.HasValue))
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            else
            {
                //Cast nullable to normal
                dateOnly = (DateTime)dateOnlyNullable;
            }

            //This LINQ will return a list of roomIDs availible to be booked, with duplicates removed

            var resultRoomList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.BookedInStudentID == null
                                 group slot.RoomID by slot.RoomID into roomGroup
                                 orderby roomGroup.Key
                                 select roomGroup.Key;

            PrintRoomAvailability(resultRoomList.ToList(), "Rooms available on " + dateOnly.ToString("dd-MM-yyy"), "<no rooms availible>");
        }

        

        //Check the ammount of bookings a staff has for a given day
        public static bool ValidateStaffBookingCount(String inputStaffID, DateTime dateOnly)
        {
            List<Slot> slotList = new SlotManager().SlotList;

            var resultSlotList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.StaffID == inputStaffID
                                 select slot;

            if (resultSlotList.Count() >= MiscellaneousUtilities.STAFF_DAILY_BOOKING_LIMIT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check the ammount of bookings a student has for a given day
        public static bool ValidateStudentBookingCount(String inputStudentID, DateTime dateOnly)
        {
            List<Slot> slotList = new SlotManager().SlotList;

            var resultSlotList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.BookedInStudentID == inputStudentID
                                 select slot;

            if (resultSlotList.Count() >= MiscellaneousUtilities.STUDENT_DAILY_BOOKING_LIMIT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check the ammount of bookings a room has for a given day
        public static bool ValidateRoomBookingCount(String roomID, DateTime dateOnly)
        {
            List<Slot> slotList = new SlotManager().SlotList;

            var resultSlotList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.RoomID == roomID
                                 select slot;

            if (resultSlotList.Count() >= MiscellaneousUtilities.ROOM_DAILY_BOOKING_LIMIT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void CheckStaffAvailability()
        {
            DateTime dateOnly;
            DateTime? dateOnlyNullable;
            List<Slot> slotList = new SlotManager().SlotList;

            Console.WriteLine("---Staff availability---");

            Console.Write("Enter date for room availability (dd-mm-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

            //VALIDATE THE DATE
            dateOnlyNullable = Slot.ValidateDate(inputDate);
            if (!(dateOnlyNullable.HasValue))
            {
                Console.WriteLine("Invalid Date");
                return;
            }
            else
            {
                //Cast nullable to normal
                dateOnly = (DateTime)dateOnlyNullable;
            }

            //VALIDATE THE STAFF ID
            if (!(ValidateStaffId(inputStaffID)))
            {
                Console.WriteLine("Invalid StaffID");
                return;
            }

            //This LINQ will return a list of roomIDs availible to be booked, with duplicates removed

            var resultRoomList = from Slot slot in slotList
                                 where slot.StartTime.Date == dateOnly.Date && slot.BookedInStudentID == null && slot.StaffID == inputStaffID
                                 select slot;
            PrintStaffAvailability(resultRoomList.ToList(), "Staff " + inputStaffID + " availability on " + dateOnly.ToString("dd-MM-yyy"), "<no slots availible>");
        }

        //LOGIC METHODS END
        
        //BOOK SLOT START

        public static void BookSlot()
        {
            Slot newSlot;

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            Console.Write("Enter student ID: ");
            string inputStudentID = Console.ReadLine();

            newSlot = ValidateBookSlot(inputRoom, inputDate, inputTime, inputStudentID);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                var SlotList = new SlotManager();
                SlotList.UpdateBooking(newSlot);
                Console.WriteLine("Booking created successfully."); ;
            }
        }

        public static Slot ValidateBookSlot(string inputRoom, string inputDate, string inputTime, string inputStudentID)
        {
            Slot slotSearchResult;
            Slot returnSlot = null;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to book slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to book slot");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }


            //VALIDATE THE STUDENTID
            if (!(ValidateStudentID(inputStudentID)))
            {
                Console.WriteLine("Unable to book slot: Invalid StudentID");
                return null;
            }

            //VALIDATE THE STUDENT BOOKING COUNT
            if (!(ValidateStudentBookingCount(inputStudentID, combinedTime)))
            {
                Console.WriteLine("Unable to book slot: StudentID has to many bookings");
                return null;
            }

            //Search for a matching slot
            slotSearchResult = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (slotSearchResult == null)
            {
                Console.WriteLine("Unable to book slot: slot does not exists");
                return null;
            }

            if(slotSearchResult.BookedInStudentID != null)
            {
                Console.WriteLine("Unable to book slot: slot already has a booking");
                return null;
            }

            slotSearchResult.BookedInStudentID = "inputStudentID";
            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            returnSlot = slotSearchResult;

            return returnSlot;
        }

        //BOOK SLOT END

        //CANCEL SLOT START

        public static void CancelBookSlot()
        {
            Slot newSlot;

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            newSlot = ValidateCancelBookSlot(inputRoom, inputDate, inputTime);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                var SlotList = new SlotManager();
                SlotList.UpdateBooking(newSlot);
                Console.WriteLine("Slot cancelled successfully."); ;
            }
        }

        public static Slot ValidateCancelBookSlot(string inputRoom, string inputDate, string inputTime)
        {
            Slot selectedSlot;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to cancel booking: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to cancel booking");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }

            //Search for a matching slot
            selectedSlot = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (selectedSlot == null)
            {
                Console.WriteLine("Unable to cancel booking: Slot does not exist");
                return null;
            }

            if (selectedSlot.BookedInStudentID == null)
            {
                Console.WriteLine("Unable to cancel booking: Slot does not have a booking");
                return null;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //Set the booking to null
            selectedSlot.BookedInStudentID = null;


            //Return updated Slot
            return selectedSlot;
        }

        //CANCEL SLOT END

        //REMOVE SLOT START

        public static void RemoveSlot()
        {
            Slot slotResult;
            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            slotResult = ValidateRemoveSlot(inputRoom, inputDate, inputTime);
            if (slotResult == null)
            {
                return;
            }
            else
            {
                Console.WriteLine("Slot removed successfully.");
            }
        }

        public static Slot ValidateRemoveSlot(string inputRoom, string inputDate, string inputTime)
        {
            Slot slotSearchResult;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to delete slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to delete slot");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }

            //Search for a matching slot
            slotSearchResult = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (slotSearchResult == null)
            {
                Console.WriteLine("Unable to delete slot: No unique match found");
                return null;
            }

            //A staff member cannot delete a slot once it has been booked by a student
            if (slotSearchResult.BookedInStudentID != null)
            {
                Console.WriteLine("Unable to delete slot: Selected slot has a booking");
                return null;
            }
            else
            {
                return slotSearchResult;
            }
        }

        //REMOVE SLOT END

        //CREATE SLOT START

        public static void CreateSlot()
        {
            Slot newSlot;

            Console.Write("Enter room name: ");
            string inputRoom = Console.ReadLine().ToUpper();

            Console.Write("Enter date for slot(dd-MM-yyyy): ");
            string inputDate = Console.ReadLine();

            Console.Write("Enter time for slot (HH:mm): ");
            string inputTime = Console.ReadLine();

            Console.Write("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

            newSlot = ValidateCreateSlot(inputRoom, inputDate, inputTime, inputStaffID);

            if (newSlot == null)
            {
                return;
            }
            else
            {
                var SlotList = new SlotManager();
                SlotList.CreateSlot(newSlot);
                Console.WriteLine("Slot created successfully."); ;
            }
        }


        public static Slot ValidateCreateSlot(string inputRoom, string inputDate, string inputTime, string inputStaffID)
        {
            Slot slotSearchResult;
            Slot returnSlot = null;
            DateTime combinedTime;
            DateTime? combinedTimeNullable;

            //VALIDATE THE ROOM
            if (!(Room.ValidateRoom(inputRoom)))
            {
                Console.WriteLine("Unable to create slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            combinedTimeNullable = Slot.ProcessDate(inputDate, inputTime, "Unable to create slot");
            if (!(combinedTimeNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                combinedTime = (DateTime)combinedTimeNullable;
            }

            //VALIDATE THE STAFF ID
            if (!(ValidateStaffId(inputStaffID)))
            {
                Console.WriteLine("Unable to create slot: Invalid StaffID");
                return null;
            }

            //VALIDATE THE BOOKING COUNT
            if (!(ValidateStaffBookingCount(inputStaffID, combinedTime)))
            {
                Console.WriteLine("Unable to create slot: StaffID has to many bookings");
                return null;
            }

            //VALIDATE THE BOOKING COUNT
            if (!(ValidateRoomBookingCount(inputRoom, combinedTime)))
            {
                Console.WriteLine("Unable to create slot: Room has to many bookings");
                return null;
            }


            //Search for a matching slot
            slotSearchResult = GetSlot(new SlotManager().SlotList, inputRoom, combinedTime);

            //if slotResult is null it means no match was found - or there was to many matches
            if (slotSearchResult != null)
            {
                Console.WriteLine("Unable to create slot: Matching slot already exists");
                return null;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            returnSlot = new Slot(inputRoom, combinedTime, inputStaffID);

            return returnSlot;
        }

        //CREATE SLOT END

        //PRINT METHODS START

        public static void PrintStaffAvailability(List<Slot> slotList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", "Room name", "Start time", "End time"));
            if (slotList.Any())
            {
                foreach (Slot selectedSlot in slotList)
                {
                    Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", selectedSlot.RoomID, selectedSlot.StartTime.ToString("HH:mm"), selectedSlot.StartTime.AddHours(1).ToString("HH:mm")));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        //Print out a list of users
        public static void PrintUserList(List<User> userList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", "ID", "Name", "Email"));

            if (userList.Any())
            {
                foreach (User selectedUser in userList)
                {
                    Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}", selectedUser.UserID, selectedUser.Name, selectedUser.Email));
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
            Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", "Room name"));

            //Check if any rooms currently exist
            if (roomList.Any())
            {
                foreach (Room selectedRoom in roomList)
                {
                    Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", selectedRoom.RoomID));
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
            Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", "Room name", "Start time", "End time", "Staff ID", "Bookings"));
            if (slotList.Any())
            {
                foreach (Slot selectedSlot in slotList)
                {
                    Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}", selectedSlot.RoomID, selectedSlot.StartTime.ToString("HH:mm"), selectedSlot.StartTime.AddHours(1).ToString("HH:mm"), selectedSlot.StaffID, selectedSlot.BookedInStudentID));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }

        public static void PrintRoomAvailability(List<string> roomList, String title, String error)
        {
            Console.WriteLine(title);
            Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", "Room name"));
            if (roomList.Any())
            {
                foreach (string selectedRoom in roomList)
                {
                    Console.WriteLine(String.Format(MiscellaneousUtilities.PRINT_INDENT + "{0,-20}", selectedRoom));
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
        }
        //PRINT METHOD END
    }

}
