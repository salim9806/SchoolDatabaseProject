USE [School2Database]



-- (2)"Skolan vill kunna ta fram en översikt över all personal där det framgår namn och vilka befattningar 
-- de har samt hur många år de har arbetat på skolan."
SELECT Id, FirstName, LastName, WorksInDepartment, STRING_AGG(Title, ',') AS Positions, DATEDIFF(YY, StartedDate, GETDATE()) AS Worked_Years FROM Personnel p
left join PersonnelOccupation po ON po.PersonnelId = p.Id
left join Occupation o ON o.Title = po.OccupationTitle
GROUP BY Id, FirstName,LastName, StartedDate, WorksInDepartment
ORDER BY Id

-- (2)"Administratören vill också ha möjlighet att spara ner ny personal." 
-- skapar en procedure för att admin ska kunna använda den att skapa nya personal
GO
CREATE PROCEDURE AddNewPersonnel
	@FirstName nvarchar(10),
	@LastName nvarchar(10),
	@Department nvarchar(20),
	@StartedDate date,
	@Occupations nvarchar(300)
AS
BEGIN
	DECLARE @PersonnelId int;
	INSERT Personnel VALUES(@FirstName, @LastName, @Department, @StartedDate);
	SELECT @PersonnelId = IDENT_CURRENT('Personnel');

	INSERT INTO PersonnelOccupation SELECT @PersonnelId as PersonnelId, value OccupationTitle FROM STRING_SPLIT(@Occupations, ',');

END
GO
-- till ex.
EXEC AddNewPersonnel 'Mr.Bean', 'Hatchinson', 'Avd 5', '2022-01-14', 'Kurator,Sjuksköterska,Rector'



-- (3) "Vi vill spara ner elever och se vilken klass de går i"
-- spara ner elev
INSERT INTO Student VALUES('Berra', 'olsson', '7A', '1999-06-15', 1411)
-- se om datan finns sparad
SELECT FirstName, LastName, BelongToClass FROM Student 
--  (3)"vill kunna spara ner betyg för en elev i varje kurs de läst"
-- hämta primär nyckeln av den senaste sparade elev
DECLARE @lastStudentInserted int;
SELECT @lastStudentInserted = IDENT_CURRENT('Student')
-- spara ner betyg för en elev i en kurs Matematik och svenska 1
INSERT INTO Grade VALUES('C', @lastStudentInserted, 'Matematik', 9, DEFAULT)
INSERT INTO Grade VALUES('B', @lastStudentInserted, 'Svenska 1', 10, DEFAULT)

--(3)"Vi vill kunna se vilken lärare som satt betyget."
--(3)"Betyg ska också ha ett datum då de satts"
SELECT CONCAT(st.FirstName, ' ', st.LastName) as Student, 
		TakenCourse, 
		Rating, 
		CONCAT(p.FirstName, ' ' , p.LastName) as Teacher, 
		TimeStamp 
FROM Student st
join Grade g ON g.StudentId = st.Id
join Personnel p ON p.Id = g.AppointedBy
where st.FirstName = 'Berra' AND st.LastName = 'olsson'


--(7)Hur mycket betalar respektive avdelning ut i lön varje månad?
SELECT DepartmentName ,SUM(Salary) as Monthly_Salary FROM Department d
JOIN Personnel p ON d.DepartmentName = p.WorksInDepartment
JOIN PersonnelOccupation po ON po.PersonnelId = P.Id
JOIN Occupation o ON o.Title = po.OccupationTitle 
GROUP BY d.DepartmentName

--(8)Hur mycket är medellönen för de olika avdelningarna?
SELECT DepartmentName ,AVG(Salary) as Average_Salary FROM Department d
JOIN Personnel p ON d.DepartmentName = p.WorksInDepartment
JOIN PersonnelOccupation po ON po.PersonnelId = P.Id
JOIN Occupation o ON o.Title = po.OccupationTitle 
GROUP BY d.DepartmentName

--(9)Skapa en Stored Procedure som tar emot ett Id och returnerar viktig information om 
--den elev som är registrerad med aktuellt id. (SQL i SSMS)
GO
CREATE PROCEDURE GetStudentInfoById
	@StudentId int
AS 
BEGIN
	SET NOCOUNT ON	
	SELECT CONCAT(FirstName, ' ', LastName) as StudentName, BelongToClass, DateOfBirth FROM Student
	WHERE Id = @StudentId
END

GO

EXEC GetStudentInfoById 2

--(10)Sätt betyg på en elev genom att använda Transactions ifall något går fel. (SQL i SSMS)
BEGIN TRANSACTION

INSERT INTO Grade VALUES('C', 16, 'Idrott', 5, DEFAULT) -- den eleven har inte betyg på idrott än
--INSERT INTO Grade VALUES('E', 16, 'WebbUtv 1', 8, DEFAULT) -- den eleven har redan betyg på WebbUtv 1. borde kasta fel.

COMMIT TRANSACTION


---------------------------------------------------------



---- table with grades as letter
--SELECT CourseName, dbo.GradeAsLetter(ROUND(AVG(GLN.Number),1)) AS avgGrade, dbo.GradeAsLetter(MAX(GLN.Number)) as maxGrade, dbo.GradeAsLetter(MIN(GLN.Number)) as minGrade FROM Course C
--join Grade G on C.CourseName = G.TakenCourse
--join Student S on S.Id = G.StudentId
--inner join GradeLetterNumberMap GLN on GLN.Letter = G.Rating
--GROUP BY CourseName

---- table with grades as letter
--SELECT CourseName, ROUND(AVG(GLN.Number),1) AS avgGrade, MAX(GLN.Number) as maxGrade, MIN(GLN.Number) as minGrade FROM Course C
--join Grade G on C.CourseName = G.TakenCourse
--join Student S on S.Id = G.StudentId
--inner join GradeLetterNumberMap GLN on GLN.Letter = G.Rating
--GROUP BY CourseName