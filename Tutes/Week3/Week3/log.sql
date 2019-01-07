CREATE TABLE log 
(
id int IDENTITY NOT NULL,
ip varchar(15) NOT NULL,
time datetime NOT NULL,
request varchar(1024) NOT NULL,
status int NOT NULL,
size int,
primary key (id)
);

CREATE TABLE ip
(
id int IDENTITY NOT NULL,
ip varchar(15) NOT NULL,
hostname varchar(128) NOT NULL,
primary key (id)
);

CREATE TABLE status
(
id int IDENTITY NOT NULL,
status int NOT NULL,
description varchar(256) NOT NULL,
primary key (id)
);

SELECT * FROM information_schema.tables;

SELECT * FROM information_schema.columns WHERE table_name = 'log';

INSERT INTO log VALUES
(
'131.170.24.42',
'2003-11-28 17:09:40',
'GET / HTTP/1.1',
200,
1526
);

INSERT INTO log VALUES
(
'131.170.27.121',
'2003-11-28 17:10:29',
'GET /dev/ HTTP/1.1',
404,
530
);

INSERT INTO ip VALUES
(
'131.170.24.42',
'yallara.cs.rmit.edu.au'
);

INSERT INTO ip VALUES
(
'131.170.27.121',
'dellis.cs.rmit.edu.au'
);

INSERT INTO status VALUES
(
200,
'OK'
);

INSERT INTO status VALUES
(
404,
'Page not found'
);

SELECT ip.ip, hostname, time, request, status, size 
FROM ip, log 
WHERE ip.ip = log.ip 
AND ip.ip = '131.170.24.42';

SELECT ip.ip, hostname, time, request, log.status, description, size 
FROM ip, log, status 
WHERE ip.ip = log.ip 
AND log.status = status.status 
AND log.status = 404;
