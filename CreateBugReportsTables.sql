CREATE TABLE Addresses (
	Id int not null identity(1,1) primary key,
	StreetName nvarchar(50) not null,
	StreetNumber int null,
	PostalCode char(50) not null,
	City nvarchar(50) not null
)
GO

CREATE TABLE Users (
	Id int not null identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	EmailAddress nvarchar(150) not null unique,
	Password nvarchar(max) not null,
	AddressId int not null references Addresses (Id)
)
GO

CREATE TABLE BugReports (
	Id int not null identity primary key,
	UserId int not null references Users (Id),
	Title nvarchar(50) not null,
	Content nvarchar(max) not null
)
GO