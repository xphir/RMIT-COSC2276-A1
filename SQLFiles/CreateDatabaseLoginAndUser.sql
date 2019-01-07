use master;
go

create database s1234567;
go

create login s1234567 with password='abc123', default_database=s1234567;
go

use s1234567;
go

create user s1234567 for login s1234567;
go

alter role db_owner add member s1234567;
go

--

alter login s1234567 with password='abc123';

--

alter login s1234567 with password='abc123' old_password='abc123';
