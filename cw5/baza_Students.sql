	select * from Student
	select * from Enrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy 
	where Studies.Name='Informatyka' and Enrollment.Semester =1;
go
alter procedure PromotionProcedure
@SName varchar,
@Semester int
as 
begin 
	SET XACT_ABORT ON;
	Begin tran
declare @New_Sem int,
		@prev_idEnroll int=(select IdEnrollment from Enrollment e inner join Studies s on e.IdStudy=s.IdStudy
							where s.Name=@SName and e.Semester=@Semester),
		@NewIdEnroll int,
		@IdStudy int=(select IdStudy from Studies where Name =@SName)

		

	select @New_Sem=Semester from Enrollment e inner join Studies s on e.IdStudy=s.IdStudy
		where s.Name = @SName and Semester=@Semester+1;
	
	select @NewIdEnroll=Max(IdEnrollment)+1 from Enrollment ;
	
	if @New_Sem is null
	begin 
		
		
		insert into Enrollment (IdEnrollment,Semester,IdStudy,StartDate)
		values(@NewIdEnroll,@Semester+1,@IdStudy,GETDATE())
		
	end

update Student set IdEnrollment=@NewIdEnroll where IdEnrollment=@prev_idEnroll;
end

execute PromotionProcedure Informatyka,1

-- Table: Enrollment
drop table Enrollment;
drop table Student;
drop table Studies;

CREATE TABLE Enrollment (
    IdEnrollment int  NOT NULL,
    Semester int  NOT NULL,
    IdStudy int  NOT NULL,
    StartDate date  NOT NULL,
    CONSTRAINT Enrollment_pk PRIMARY KEY  (IdEnrollment)
);
insert into Enrollment values(1,1,12,'12-10-2020'),(2,1,13,'12-10-2020'),(3,1,11,'12-10-2020'),(4,1,13,'12-10-2020'),
(5,1,13,'12-10-2020'),(6,2,12,'12-10-2020'),(7,2,13,'12-10-2020'),(8,3,11,'12-10-2020');
insert into Enrollment values(9,1,12,'12-10-2020')
-- Table: Student
CREATE TABLE Student (
    IndexNumber nvarchar(100)  NOT NULL,
    FirstName nvarchar(100)  NOT NULL,
    LastName nvarchar(100)  NOT NULL,
    BirthDate date  NOT NULL,
    IdEnrollment int  NOT NULL,
    CONSTRAINT Student_pk PRIMARY KEY  (IndexNumber)
);
insert into Student values('s19322','Ala','Malik','1999-09-20',1),
('s18933','Ola','Majewska','1998-09-19',2),
('s19433','Marcin','Piotr','1999-02-25',3),
('s17822','Daniel','Kacj','1999-07-10',4),
('s20202','Kamil','Marszalek','1999-09-03',5),
('s14324','Piotr','Pawlik','1997-12-20',6),
('s19321','Alina','Oleksy','1996-11-24',7),
('s32123','Grzegorz','Idian','1999-01-09',8);
insert into Student values('s29292','Natalia','Oko','1998-02-09',9)
-- Table: Studies
CREATE TABLE Studies (
    IdStudy int  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    CONSTRAINT Studies_pk PRIMARY KEY  (IdStudy)
);
insert into Studies values
(11,'Informatyka'),
(12,'Sztuka nowych mediow'),
(13,'Grafika komputerowa'),
(14,'Zarzadzanie');

-- foreign keys
-- Reference: Enrollment_Studies (table: Enrollment)
ALTER TABLE Enrollment ADD CONSTRAINT Enrollment_Studies
    FOREIGN KEY (IdStudy)
    REFERENCES Studies (IdStudy);

-- Reference: Student_Enrollment (table: Student)
ALTER TABLE Student ADD CONSTRAINT Student_Enrollment
    FOREIGN KEY (IdEnrollment)
    REFERENCES Enrollment (IdEnrollment);
---------------------------------------------------

