--Insert / Select Address
DECLARE @StreetName nvarchar(100) SET @StreetName = ''
DECLARE @StreetNumber int SET @StreetNumber = ''
DECLARE @PostalCode char(6) SET @PostalCode = ''
DECLARE @City nvarchar(100) SET @City = ''

IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)
	INSERT INTO Addresses OUTPUT INSERTED Id VALUES (@StreetName, @PostalCode, @City)
ELSE
	SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City

-- Insert User
DECLARE @FirstName nvarchar(100) SET @FirstName = ''
DECLARE @LastName nvarchar(100) SET @LastName = ''
DECLARE @Email nvarchar(100) SET @Email = ''
DECLARE @AddressId int SET @AddressId = ''

IF NOT EXISTS (SELECT Id FROM Users WHERE Email = @Email)
	INSERT INTO Users OUTPUT INSERTED Id VALUES (@FirstName, @LastName, @Email, @AddressId)

-- List Users
SELECT 
	u.Id, u.FirstName, u.LastName, u.Email,
	a.StreetName, a.StreetNumber, a.PostalCode, a.City
FROM Users u
JOIN Addresses a ON u.AddressId = a.id

-- Get User
SELECT 
	u.Id, u.FirstName, u.LastName, u.Email,
	a.StreetName, a.StreetNumber, a.PostalCode, a.City
FROM Users u
JOIN Addresses a ON u.AddressId = a.id
WHERE u.Email = @Email