USE [master]

GO
SET NOCOUNT on
set QUOTED_IDENTIFIER Off



IF EXISTS(SELECT * FROM sysdatabases WHERE [name] = "School2Database")
	ALTER DATABASE [School2Database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	GO
	DROP DATABASE [School2Database]

GO


CREATE DATABASE [School2Database]

GO

USE [School2Database]

set QUOTED_IDENTIFIER on

GO

CREATE TABLE Department (
	DepartmentName nvarchar(20) NOT NULL PRIMARY KEY
)

INSERT INTO Department VALUES('Avd 1');
INSERT INTO Department VALUES('Avd 2');
INSERT INTO Department VALUES('Avd 3');
INSERT INTO Department VALUES('Avd 4');
INSERT INTO Department VALUES('Avd 5');
-----------------------------------------------------------

CREATE TABLE Personnel(
	Id int IDENTITY(1,1) PRIMARY KEY, 
	FirstName nvarchar(10) NOT NULL, 
	LastName nvarchar(10) NOT NULL,
	WorksInDepartment nvarchar(20) NOT NULL FOREIGN KEY REFERENCES Department(DepartmentName),
	StartedDate Date NOT null CHECK (StartedDate < GetDate())
)

INSERT INTO Personnel VALUES('Salim', 'Mohamed', 'Avd 1', '2000-07-21');
INSERT INTO Personnel VALUES('Johanna', 'Andersson', 'Avd 1', '2005-09-11');
INSERT INTO Personnel VALUES('Eva', 'Sandström', 'Avd 2', '2013-01-09');
INSERT INTO Personnel VALUES('Malin', 'Carter', 'Avd 2', '2022-06-12');
INSERT INTO Personnel VALUES('David', 'Jakopsson', 'Avd 3', '2022-04-01');
INSERT INTO Personnel VALUES('Daniel', 'Wilson', 'Avd 3', '2019-04-01');
INSERT INTO Personnel VALUES('Julie', 'Smith', 'Avd 4', '1998-06-16');
INSERT INTO Personnel VALUES('Nicole', 'Wilson', 'Avd 4', '2000-03-12');
INSERT INTO Personnel VALUES('Danielle', 'Dean', 'Avd 5', '2016-11-30');
INSERT INTO Personnel VALUES('Hunter', 'Quinn', 'Avd 1', '2018-10-11');
--------------------------------------------------------

CREATE TABLE Occupation(
	Title nvarchar(15) NOT NULL PRIMARY KEY,
	Salary int NOT NULL
)

INSERT INTO Occupation VALUES('Lärare', 25000)
INSERT INTO Occupation VALUES('Rector', 140000)
INSERT INTO Occupation VALUES('Sjuksköterska', 22000)
INSERT INTO Occupation VALUES('Kurator', 23500)
INSERT INTO Occupation VALUES('Yrkesvägledare', 28000)
INSERT INTO Occupation VALUES('IT-tekniker', 45000)

GO 
------------------------------------------------------

CREATE TABLE PersonnelOccupation (
	PersonnelId int NOT NULL FOREIGN KEY REFERENCES Personnel(Id),
	OccupationTitle nvarchar(15) NOT NULL FOREIGN KEY REFERENCES Occupation(Title),
	PRIMARY KEY (PersonnelId, OccupationTitle)
)

INSERT INTO PersonnelOccupation VALUES(1,'Lärare')
--INSERT INTO PersonnelOccupation VALUES(3,'Lärare')
INSERT INTO PersonnelOccupation VALUES(2,'Lärare')
INSERT INTO PersonnelOccupation VALUES(6,'Lärare')
INSERT INTO PersonnelOccupation VALUES(8,'Lärare')
INSERT INTO PersonnelOccupation VALUES(9,'Lärare')
INSERT INTO PersonnelOccupation VALUES(10,'Lärare')
INSERT INTO PersonnelOccupation VALUES(7,'Lärare')
INSERT INTO PersonnelOccupation VALUES(4,'Lärare')
INSERT INTO PersonnelOccupation VALUES(5,'Lärare')

INSERT INTO PersonnelOccupation VALUES(10,'Rector')

INSERT INTO PersonnelOccupation VALUES(2,'Sjuksköterska')
INSERT INTO PersonnelOccupation VALUES(7,'Sjuksköterska')
INSERT INTO PersonnelOccupation VALUES(9,'Sjuksköterska')

INSERT INTO PersonnelOccupation VALUES(3,'Kurator')
INSERT INTO PersonnelOccupation VALUES(6,'Kurator')
INSERT INTO PersonnelOccupation VALUES(7,'Kurator')

INSERT INTO PersonnelOccupation VALUES(1,'Yrkesvägledare')

Go
--------------------------------------------------------------------------------

--CREATE VIEW Vw_PersonnelOccupation AS
--SELECT * FROM Personnel p
--left join PersonnelOccupation op ON  p.Id = op.PersonnelId
--left join Occupation o ON op.OccupationTitle = o.Title;

GO
----------------------------------------------------------------------------------
CREATE TABLE Class (
	ClassName char(2) NOT NULL PRIMARY KEY
)

INSERT INTO Class VALUES('7A')
INSERT INTO Class VALUES('7B')
INSERT INTO Class VALUES('7C')
INSERT INTO Class VALUES('8A')
INSERT INTO Class VALUES('8B')
INSERT INTO Class VALUES('8C')
INSERT INTO Class VALUES('9A')
INSERT INTO Class VALUES('9B')
INSERT INTO Class VALUES('9C')

GO
---------------------------------------------------------

CREATE TABLE Student (
	Id int identity(1,1) NOT NULL PRIMARY KEY,
	FirstName nvarchar(10) NOT NULL,
	LastName nvarchar(10) NOT NULL,
	BelongToClass char(2) NOT NULL FOREIGN KEY REFERENCES Class(ClassName),
	DateOfBirth date NOT NULL,
	SocialSecurity numeric(4,0) NOT NULL,
)

INSERT INTO Student VALUES('Alan', 'Morales', '8B','1998-09-12',1013)
INSERT INTO Student VALUES('Judith', 'Orr', '9B','1998-07-22', 1825)
INSERT INTO Student VALUES('David', 'Price', '9C','1998-11-01', 1202)
INSERT INTO Student VALUES('Tyler', 'Maynard', '9B','1998-03-05', 1415)
INSERT INTO Student VALUES('Troy', 'Jones', '9B','1998-08-19', 9823)
INSERT INTO Student VALUES('Teresa', 'Riddle', '9A','1998-10-26', 8724)
INSERT INTO Student VALUES('Cassandra', 'Campbell', '9A','1998-06-16',1673)
INSERT INTO Student VALUES('Benjamin', 'Hudson', '7B','1998-09-10', 4824)
INSERT INTO Student VALUES('Mark', 'Simpson', '8A','1998-02-11', 9544)
INSERT INTO Student VALUES('Christ', 'Phelps', '7A','1998-01-22', 3724)
INSERT INTO Student VALUES('Nicole', 'Pittman', '9A','1998-04-14',1134)
INSERT INTO Student VALUES('Kyle', 'Sanchez', '9C','1998-12-12', 3374)
INSERT INTO Student VALUES('Michael', 'Williams', '9B','1998-04-08',8343)
INSERT INTO Student VALUES('Nicole', 'Osborn', '7C','1998-10-25',8348)
INSERT INTO Student VALUES('Samuel', 'Scott', '8C','1998-08-27',4245)
INSERT INTO Student VALUES('Fred', 'Romero', '7A','1998-01-29',7582)

GO
--------------------------------------------------------------------

CREATE TABLE Course (
	CourseName nvarchar(10) NOT NULL PRIMARY KEY,
	IsActive bit NOT NULL,
)

INSERT INTO Course VALUES('Matematik', 1)
INSERT INTO Course VALUES('Svenska 1', 1)
INSERT INTO Course VALUES('Svenska 2', 1)
INSERT INTO Course VALUES('Fysik', 1)
INSERT INTO Course VALUES('Biologi', 1)
INSERT INTO Course VALUES('Kemi', 1)
INSERT INTO Course VALUES('WebbUtv 1', 1)
INSERT INTO Course VALUES('Idrott', 1)

-----------------------------------------------------------------------

CREATE TABLE Grade(
	Rating char(1) NOT NULL CHECK(Rating in ('A', 'B', 'C', 'D', 'E', 'F')),
	StudentId int NOT NULL FOREIGN KEY REFERENCES Student(Id),
	TakenCourse nvarchar(10) NOT NULL FOREIGN KEY REFERENCES Course(CourseName),
	AppointedBy int NOT NULL FOREIGN KEY REFERENCES Personnel(Id),
	[TimeStamp] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
	PRIMARY KEY(StudentId, TakenCourse)
)
GO
--trigger: se om den sätter beyget är en lärare
CREATE TRIGGER Check_If_Appointee_is_Teacher ON Grade
	INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF 'Lärare' != ALL(SELECT OccupationTitle  FROM INSERTED
					   LEFT JOIN PersonnelOccupation po ON po.PersonnelId = AppointedBy)
		THROW 50001, N'The appointee has to be a teacher!',1;
	ELSE	
		-- check if it was INSERT or UPDATE	operation
		IF(EXISTS(SELECT * FROM DELETED))
			BEGIN
			UPDATE Grade SET Rating = i.Rating, 
							 TakenCourse = i.TakenCourse,
							 StudentId = i.StudentId,
							 AppointedBy = i.AppointedBy
					     FROM Grade as g
						 inner join INSERTED as i 
						 on g.StudentId = i.StudentId AND g.TakenCourse = i.TakenCourse
			END
		ELSE
			BEGIN
			INSERT INTO Grade SELECT * FROM INSERTED
			END
END

GO

INSERT INTO Grade VALUES('E', 1, 'Matematik', 9, DEFAULT)
INSERT INTO Grade VALUES('B', 1, 'Svenska 1', 10, DEFAULT)
INSERT INTO Grade VALUES('E', 1, 'Svenska 2', 5, DEFAULT)
INSERT INTO Grade VALUES('A', 1, 'Fysik', 1, DEFAULT)
INSERT INTO Grade VALUES('E', 1, 'Biologi', 6, DEFAULT)
INSERT INTO Grade VALUES('D', 1, 'Kemi', 8, DEFAULT)
INSERT INTO Grade VALUES('D', 1, 'WebbUtv 1', 8, DEFAULT)
INSERT INTO Grade VALUES('A', 1, 'Idrott', 7, DEFAULT)
INSERT INTO Grade VALUES('E', 2, 'Matematik', 2, DEFAULT)
INSERT INTO Grade VALUES('F', 2, 'Svenska 1', 4, DEFAULT)
INSERT INTO Grade VALUES('E', 2, 'Svenska 2', 4, DEFAULT)
INSERT INTO Grade VALUES('F', 2, 'Fysik', 7, DEFAULT)
INSERT INTO Grade VALUES('B', 2, 'Biologi', 6, DEFAULT)
INSERT INTO Grade VALUES('E', 2, 'Kemi', 10, DEFAULT)
INSERT INTO Grade VALUES('A', 2, 'WebbUtv 1', 7, DEFAULT)
INSERT INTO Grade VALUES('B', 2, 'Idrott', 7, DEFAULT) 
INSERT INTO Grade VALUES('A', 3, 'Matematik', 4, DEFAULT)
INSERT INTO Grade VALUES('D', 3, 'Svenska 1', 4, DEFAULT)
INSERT INTO Grade VALUES('B', 3, 'Svenska 2', 7, DEFAULT)
INSERT INTO Grade VALUES('E', 3, 'Fysik', 7, DEFAULT)
INSERT INTO Grade VALUES('C', 3, 'Biologi', 9, DEFAULT)
INSERT INTO Grade VALUES('C', 3, 'Kemi', 2, DEFAULT)
INSERT INTO Grade VALUES('C', 3, 'WebbUtv 1', 9, DEFAULT)
INSERT INTO Grade VALUES('C', 3, 'Idrott', 4, DEFAULT)
INSERT INTO Grade VALUES('B', 4, 'Matematik', 5, DEFAULT)
INSERT INTO Grade VALUES('C', 4, 'Svenska 1', 2, DEFAULT)
INSERT INTO Grade VALUES('B', 4, 'Svenska 2', 7, DEFAULT)
INSERT INTO Grade VALUES('C', 4, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('F', 4, 'Biologi', 6, DEFAULT)
INSERT INTO Grade VALUES('E', 4, 'Kemi', 2, DEFAULT)
INSERT INTO Grade VALUES('E', 4, 'WebbUtv 1', 2, DEFAULT)
INSERT INTO Grade VALUES('A', 4, 'Idrott', 1, DEFAULT)
INSERT INTO Grade VALUES('C', 5, 'Matematik', 9, DEFAULT)
INSERT INTO Grade VALUES('E', 5, 'Svenska 1', 5, DEFAULT)
INSERT INTO Grade VALUES('B', 5, 'Svenska 2', 8, DEFAULT)
INSERT INTO Grade VALUES('C', 5, 'Fysik', 1, DEFAULT)
INSERT INTO Grade VALUES('D', 5, 'Biologi', 6, DEFAULT)
INSERT INTO Grade VALUES('F', 5, 'Kemi', 9, DEFAULT)
INSERT INTO Grade VALUES('F', 5, 'WebbUtv 1', 8, DEFAULT)
INSERT INTO Grade VALUES('C', 5, 'Idrott', 4, DEFAULT)
INSERT INTO Grade VALUES('A', 6, 'Matematik', 5, DEFAULT)
INSERT INTO Grade VALUES('E', 6, 'Svenska 1', 8, DEFAULT)
INSERT INTO Grade VALUES('B', 6, 'Svenska 2', 2, DEFAULT)
INSERT INTO Grade VALUES('C', 6, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('F', 6, 'Biologi', 1, DEFAULT)
INSERT INTO Grade VALUES('C', 6, 'Kemi', 7, DEFAULT)
INSERT INTO Grade VALUES('F', 6, 'WebbUtv 1', 6, DEFAULT)
INSERT INTO Grade VALUES('A', 6, 'Idrott', 7, DEFAULT)
INSERT INTO Grade VALUES('E', 7, 'Matematik', 4, DEFAULT)
INSERT INTO Grade VALUES('F', 7, 'Svenska 1', 6, DEFAULT)
INSERT INTO Grade VALUES('B', 7, 'Svenska 2', 2, DEFAULT)
INSERT INTO Grade VALUES('A', 7, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('F', 7, 'Biologi', 7, DEFAULT)
INSERT INTO Grade VALUES('D', 7, 'Kemi', 10, DEFAULT)
INSERT INTO Grade VALUES('E', 7, 'WebbUtv 1', 5, DEFAULT)
INSERT INTO Grade VALUES('D', 7, 'Idrott', 8, DEFAULT)
INSERT INTO Grade VALUES('B', 8, 'Matematik', 4, DEFAULT)
INSERT INTO Grade VALUES('A', 8, 'Svenska 1', 2, DEFAULT)
INSERT INTO Grade VALUES('C', 8, 'Svenska 2', 4, DEFAULT)
INSERT INTO Grade VALUES('E', 8, 'Fysik', 2, DEFAULT)
INSERT INTO Grade VALUES('C', 8, 'Biologi', 1, DEFAULT)
INSERT INTO Grade VALUES('B', 8, 'Kemi', 2, DEFAULT)
INSERT INTO Grade VALUES('D', 8, 'WebbUtv 1', 9, DEFAULT)
INSERT INTO Grade VALUES('B', 8, 'Idrott', 1, DEFAULT)
INSERT INTO Grade VALUES('B', 9, 'Matematik', 7, DEFAULT)
INSERT INTO Grade VALUES('A', 9, 'Svenska 1', 6, DEFAULT)
INSERT INTO Grade VALUES('E', 9, 'Svenska 2', 8, DEFAULT)
INSERT INTO Grade VALUES('A', 9, 'Fysik', 10, DEFAULT)
INSERT INTO Grade VALUES('C', 9, 'Biologi', 4, DEFAULT)
INSERT INTO Grade VALUES('E', 9, 'Kemi', 10, DEFAULT)
INSERT INTO Grade VALUES('E', 9, 'WebbUtv 1', 5, DEFAULT)
INSERT INTO Grade VALUES('E', 9, 'Idrott', 6, DEFAULT)
INSERT INTO Grade VALUES('D', 10, 'Matematik', 2, DEFAULT)
INSERT INTO Grade VALUES('C', 10, 'Svenska 1', 6, DEFAULT)
INSERT INTO Grade VALUES('D', 10, 'Svenska 2', 10, DEFAULT)
INSERT INTO Grade VALUES('F', 10, 'Fysik', 4, DEFAULT)
INSERT INTO Grade VALUES('E', 10, 'Biologi', 7, DEFAULT)
INSERT INTO Grade VALUES('A', 10, 'Kemi', 2, DEFAULT)
INSERT INTO Grade VALUES('F', 10, 'WebbUtv 1', 10, DEFAULT)
INSERT INTO Grade VALUES('F', 10, 'Idrott', 2, DEFAULT)
INSERT INTO Grade VALUES('A', 11, 'Matematik', 6, DEFAULT)
INSERT INTO Grade VALUES('E', 11, 'Svenska 1', 10, DEFAULT)
INSERT INTO Grade VALUES('C', 11, 'Svenska 2', 1, DEFAULT)
INSERT INTO Grade VALUES('C', 11, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('B', 11, 'Biologi', 2, DEFAULT)
INSERT INTO Grade VALUES('E', 11, 'Kemi', 1, DEFAULT)
INSERT INTO Grade VALUES('E', 11, 'WebbUtv 1', 8, DEFAULT)
INSERT INTO Grade VALUES('E', 11, 'Idrott', 1, DEFAULT)
INSERT INTO Grade VALUES('B', 12, 'Matematik', 7, DEFAULT)
INSERT INTO Grade VALUES('E', 12, 'Svenska 1', 4, DEFAULT)
INSERT INTO Grade VALUES('D', 12, 'Svenska 2', 7, DEFAULT)
INSERT INTO Grade VALUES('D', 12, 'Fysik', 9, DEFAULT)
INSERT INTO Grade VALUES('E', 12, 'Biologi', 8, DEFAULT)
INSERT INTO Grade VALUES('F', 12, 'Kemi', 2, DEFAULT)
INSERT INTO Grade VALUES('E', 12, 'WebbUtv 1', 10, DEFAULT)
INSERT INTO Grade VALUES('A', 12, 'Idrott', 5, DEFAULT)
INSERT INTO Grade VALUES('C', 13, 'Matematik', 6, DEFAULT)
INSERT INTO Grade VALUES('F', 13, 'Svenska 1', 1, DEFAULT)
INSERT INTO Grade VALUES('E', 13, 'Svenska 2', 9, DEFAULT)
INSERT INTO Grade VALUES('B', 13, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('E', 13, 'Biologi', 9, DEFAULT)
INSERT INTO Grade VALUES('B', 13, 'Kemi', 4, DEFAULT)
INSERT INTO Grade VALUES('B', 13, 'WebbUtv 1', 9, DEFAULT)
INSERT INTO Grade VALUES('E', 13, 'Idrott', 9, DEFAULT)
INSERT INTO Grade VALUES('A', 14, 'Matematik', 2, DEFAULT)
INSERT INTO Grade VALUES('F', 14, 'Svenska 1', 10, DEFAULT)
INSERT INTO Grade VALUES('B', 14, 'Svenska 2', 7, DEFAULT)
INSERT INTO Grade VALUES('E', 14, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('A', 14, 'Biologi', 5, DEFAULT)
INSERT INTO Grade VALUES('B', 14, 'Kemi', 6, DEFAULT)
INSERT INTO Grade VALUES('A', 14, 'WebbUtv 1', 1, DEFAULT)
INSERT INTO Grade VALUES('E', 14, 'Idrott', 4, DEFAULT)
INSERT INTO Grade VALUES('A', 15, 'Matematik', 4, DEFAULT)
INSERT INTO Grade VALUES('E', 15, 'Svenska 1', 1, DEFAULT)
INSERT INTO Grade VALUES('F', 15, 'Svenska 2', 7, DEFAULT)
INSERT INTO Grade VALUES('C', 15, 'Fysik', 5, DEFAULT)
INSERT INTO Grade VALUES('C', 15, 'Biologi', 2, DEFAULT)
INSERT INTO Grade VALUES('E', 15, 'Kemi', 6, DEFAULT)
INSERT INTO Grade VALUES('C', 15, 'WebbUtv 1', 2, DEFAULT)
INSERT INTO Grade VALUES('A', 15, 'Idrott', 10, DEFAULT)
INSERT INTO Grade VALUES('A', 16, 'Matematik', 2, DEFAULT)
INSERT INTO Grade VALUES('A', 16, 'Svenska 1', 6, DEFAULT)
INSERT INTO Grade VALUES('F', 16, 'Svenska 2', 9, DEFAULT)
INSERT INTO Grade VALUES('B', 16, 'Fysik', 7, DEFAULT)
INSERT INTO Grade VALUES('E', 16, 'Biologi', 5, DEFAULT)
INSERT INTO Grade VALUES('F', 16, 'Kemi', 2, DEFAULT)
INSERT INTO Grade VALUES('E', 16, 'WebbUtv 1', 8, DEFAULT)
-----------------------------------------------------------------------------

-- table to use to perform calculation on grades. .ex calculating the average grade.
CREATE TABLE GradeLetterNumberMap (
	Letter varchar(1) NOT NULL PRIMARY KEY,
	Number NUMERIC(3,1) NOT NULL
)

INSERT INTO GradeLetterNumberMap VALUES('A', 20.0)
INSERT INTO GradeLetterNumberMap VALUES('B', 17.5)
INSERT INTO GradeLetterNumberMap VALUES('C', 15.0)
INSERT INTO GradeLetterNumberMap VALUES('D', 12.5)
INSERT INTO GradeLetterNumberMap VALUES('E', 10.0)
INSERT INTO GradeLetterNumberMap VALUES('F', 0.0)


-- a user defined function to convert grade in form of number to letter corresponding to table GradeLetterNumberMap
GO
CREATE FUNCTION GradeAsLetter(@gradeNumber NUMERIC(3,1))
RETURNS varchar(1)
AS
BEGIN
	DECLARE @closestLetter varchar(1)

	SET @closestLetter = (SELECT TOP 1 Letter FROM GradeLetterNumberMap
	ORDER BY ABS(@gradeNumber - Number))

	RETURN @closestLetter;
END
GO