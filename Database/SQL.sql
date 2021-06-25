create table Users (
	id int IDENTITY(1,1) PRIMARY KEY,
	first_name varchar(255) NOT NULL,
	last_name varchar(255) NOT NULL,
	email varchar(255) NOT NULL,
	password varchar(255) NOT NULL,
	salt varchar(255) NOT NULL,
	role varchar(255) NOT NULL
);

create table Lessons (
	studentID int foreign key references Users(id),
	lesson1read int not null default 0,
	lesson2read int not null default 0,
	lesson3read int not null default 0,
);

create table Exercises(
	studentID int foreign key references Users(id),
	exercise1score int not null default 0,
	exercise1tries int not null default 0,
	exercise2score int not null default 0,
	exercise2tries int not null default 0,
	exercise3score int not null default 0,
	exercise3tries int not null default 0,
	exercise4score int not null default 0,
	exercise4tries int not null default 0,
);

create table Questions(
	id int IDENTITY(1,1) PRIMARY KEY,
	lesson int not null,
	leftNumber int not null,
	rightNumber int not null
);

create table Mistakes(
	id int IDENTITY(1,1) PRIMARY KEY,
	studentID int foreign key references Users(id),
	questionID int foreign key references Questions(id),
	answer varchar(255) not null
);

create table Game(
	id int IDENTITY(1,1) PRIMARY KEY,
	studentID int foreign key references Users(id),
	score1 int not null default 0,
	score2 int not null default 0,
	score3 int not null default 0
);


insert into Questions(lesson, leftNumber, rightNumber) values (1, 2, 2);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 2, 5);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 2, 7);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 1, 10);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 1, 8);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 3, 8);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 3, 1);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 1, 1);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 3, 10);
insert into Questions(lesson, leftNumber, rightNumber) values (1, 3, 2);

insert into Questions(lesson, leftNumber, rightNumber) values (2, 4, 2);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 5, 5);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 4, 7);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 6, 10);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 6, 8);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 5, 8);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 4, 3);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 6, 9);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 5, 9);
insert into Questions(lesson, leftNumber, rightNumber) values (2, 5, 10);

insert into Questions(lesson, leftNumber, rightNumber) values (3, 10, 10);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 7, 5);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 9, 7);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 8, 8);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 9, 8);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 9, 3);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 7, 6);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 10, 2);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 8, 2);
insert into Questions(lesson, leftNumber, rightNumber) values (3, 7, 3);




