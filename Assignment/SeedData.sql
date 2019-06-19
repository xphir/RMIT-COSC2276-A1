insert into Room (RoomID) values ('A');
insert into Room (RoomID) values ('B');
insert into Room (RoomID) values ('C');
insert into Room (RoomID) values ('D');

insert into [User] (UserID, Name, Email) values ('e12345', 'Matt', 'e12345@rmit.edu.au');
insert into [User] (UserID, Name, Email) values ('e56789', 'Joe', 'e56789@rmit.edu.au');
insert into [User] (UserID, Name, Email) values ('e54321', 'Rod', 'e54321@rmit.edu.au');
insert into [User] (UserID, Name, Email) values ('e98765', 'Alex', 'e98765@rmit.edu.au');

insert into [User] (UserID, Name, Email) values ('s1234567', 'Kevin', 's1234567@student.rmit.edu.au');
insert into [User] (UserID, Name, Email) values ('s4567890', 'Olivier', 's4567890@student.rmit.edu.au');
insert into [User] (UserID, Name, Email) values ('s7654321', 'Elliot', 's7654321@student.rmit.edu.au');
insert into [User] (UserID, Name, Email) values ('s0987654', 'Philip', 's0987654@student.rmit.edu.au');

insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('A', '20180115 12:00:00', 'e12345', 's1234567');
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('A', '20180115 11:00:00', 'e12345', 's7654321');
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('B', '20180115 10:00:00', 'e54321', null);
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('C', '20180115 09:00:00', 'e98765', null);
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('D', '20180115 13:00:00', 'e12345', 's4567890');
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('A', '20180114 12:00:00', 'e12345', 's1234567');
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('A', '20180114 11:00:00', 'e12345', null);
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('B', '20180113 10:00:00', 'e54321', 's7654321');
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('C', '20180113 09:00:00', 'e98765', null);
insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('D', '20180112 13:00:00', 'e12345', 's4567890');

insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ("A", {9/12/2018 1:00:00 PM},"s12345", null);

insert into Slot (RoomID, StartTime, StaffID, BookedInStudentID) values ('D', '20180112 11:00:00', 'e12345');