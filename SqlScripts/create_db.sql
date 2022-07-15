create database Clinic;
use Clinic;

create table Doctor(
	Id int identity(1,1) constraint PK_Doctor primary key,
	Name nvarchar(255),
	Specialisation nvarchar(255),
	YearsOfExperience INT
);

create table Patient(
	Id int identity(1,1) constraint PK_Patient primary key,
	Name nvarchar(255),
	Birthdate DATETIME,
	PhoneNumber nvarchar(255)
);

create table Voucher(
	Id int identity(1,1) constraint PK_Voucher primary key,
	ReceptionTime DATETIME,
	DoctorId INT constraint FK_Voucher_Doctor references Doctor(Id),
	PatientId INT constraint FK_Voucher_Patient references Patient(Id),
);

