CREATE TABLE Addresses (
	Id int not null identity(1,1) primary key,
	StreetName nvarchar(100) not null,
	StreetNumber int null,
	PostalCode char(6) not null,
	City nvarchar(100) not null
)
GO

CREATE TABLE Users (
	Id int not null identity primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	EmailAddress nvarchar(100) not null unique,
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