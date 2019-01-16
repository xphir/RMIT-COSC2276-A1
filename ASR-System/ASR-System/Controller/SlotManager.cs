using System;
using System.Collections.Generic;
using System.Linq;
using ASR_System.Model;
using ASR_System.Utilities;

namespace ASR_System.Controller
{
    public class SlotManager
    {
        public List<Slot> SlotList { get; }

        public SlotManager()
        {
            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "select * from Slot";

                SlotList = command.GetDataTable().Select().Select(x =>
                    new Slot((string)x["RoomID"], (DateTime)x["StartTime"], (string)x["StaffID"], x["BookedInStudentID"] is DBNull ? null : (string)x["BookedInStudentID"])).ToList();
            }
        }

        public void UpdateBooking(Slot slot)
        {
            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    "update Slot set BookedInStudentID = @bookedInStudentID " +
                    "where RoomID = @roomID and StartTime = @startTime";

                command.Parameters.AddWithValue("startTime", slot.StartTime);
                command.Parameters.AddWithValue("roomID", slot.RoomID);
                //Workaround to accept a null value
                if (string.IsNullOrEmpty(slot.BookedInStudentID))
                {
                    command.Parameters.AddWithValue("bookedInStudentID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("bookedInStudentID", slot.BookedInStudentID);
                }

                command.ExecuteNonQuery();
            }
        }

        public void CreateSlot(Slot slot)
        {


            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                connection.Open();


                var command = connection.CreateCommand();
                command.CommandText =
                    "insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) " +
                    "values (@roomID, @startTime, @staffID, @bookedInStudentID);";

                command.Parameters.AddWithValue("roomID", slot.RoomID);
                command.Parameters.AddWithValue("startTime", slot.StartTime);
                command.Parameters.AddWithValue("staffID", slot.StaffID);
                //Workaround to accept a null value
                if (string.IsNullOrEmpty(slot.BookedInStudentID))
                {
                    command.Parameters.AddWithValue("bookedInStudentID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("bookedInStudentID", slot.BookedInStudentID);
                }

                command.ExecuteNonQuery();
            }
        }

        public void DeleteSlot(Slot slot)
        {
            using (var connection = SQLConnectionSingleton.Instance().Connection.CreateConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    "delete from Slot" +
                    "where RoomID = @roomID and StartTime = @startTime;";
                command.Parameters.AddWithValue("roomID", slot.RoomID);
                command.Parameters.AddWithValue("startTime", slot.StartTime);

                command.ExecuteNonQuery();
            }
        }
    }
}
