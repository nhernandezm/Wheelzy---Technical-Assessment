CREATE DATABASE CarSales;
GO

USE CarSales;
GO

CREATE TABLE dbo.Makes
(
	MakeID INT CONSTRAINT pk_Makes_MakeID  PRIMARY KEY IDENTITY,
	MakeName NVARCHAR(50)
);
GO

CREATE TABLE dbo.Models
(
	ModelID INT CONSTRAINT pk_Models_ModelID PRIMARY KEY IDENTITY,
	ModelName NVARCHAR(50),
);
GO

CREATE TABLE dbo.SubModels
(
	SubModelID INT CONSTRAINT pk_SubModels_SubModelID PRIMARY KEY IDENTITY,
	SubModelName NVARCHAR(50),
);
GO

CREATE TABLE dbo.Locations
(
	LocationID INT CONSTRAINT pk_Locations_LocationID PRIMARY KEY IDENTITY,
	LocationName NVARCHAR(50),
	ZipCode NVARCHAR(50)
);
GO

CREATE TABLE Costumers
(
	CostumerID INT CONSTRAINT pk_Costumers_CostumerID PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50),
	Phone NVARCHAR(50),
	LocationID INT FOREIGN KEY REFERENCES dbo.Locations(LocationID) NOT NULL,
);
GO

CREATE TABLE dbo.Cars
(
	CarID INT CONSTRAINT pk_Cars_CarID PRIMARY KEY IDENTITY,
	MakeID INT FOREIGN KEY REFERENCES dbo.Makes(MakeID),
	ModelID INT FOREIGN KEY REFERENCES dbo.Models(ModelID),
	SubModelID INT FOREIGN KEY REFERENCES dbo.SubModels(SubModelID),
	LocationID INT FOREIGN KEY REFERENCES dbo.Locations(LocationID),
	CostumerID INT FOREIGN KEY REFERENCES dbo.Costumers(CostumerID),
	RegistrationNumber VARCHAR(50) CONSTRAINT uq_Cars_RegistrationNumber NOT NULL UNIQUE,
	Year INT,
	Price DECIMAL(10, 2)
);
GO

CREATE TABLE dbo.status
(
	StatusID INT CONSTRAINT pk_status_StatusID PRIMARY KEY IDENTITY,
	StatusName NVARCHAR(50)
);
GO

CREATE TABLE dbo.Buyers
(
	BuyerID INT CONSTRAINT pk_Buyers_BuyerID PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50),
	Phone NVARCHAR(50),
	LocationID INT FOREIGN KEY REFERENCES dbo.Locations(LocationID) NOT NULL,
);
GO

CREATE TABLE dbo.PotentialBuyers
(
	PotentialBuyerID INT CONSTRAINT pk_PotentialBuyers_PotentialBuyerID PRIMARY KEY IDENTITY,
	BuyerID INT FOREIGN KEY REFERENCES dbo.Buyers(BuyerID),
	amount DECIMAL(10, 2) NOT NULL,
	IsCurrentOne BIT DEFAULT 0,
	DatePickup DATETIME,
	CarID INT FOREIGN KEY REFERENCES dbo.Cars(CarID),
	StatusID INT FOREIGN KEY REFERENCES dbo.status(StatusID)
);
GO

CREATE TABLE dbo.logs
(
	LogID INT CONSTRAINT pk_logs_LogID PRIMARY KEY IDENTITY,
	LogMessage NVARCHAR(50),
	LogDate DATETIME,
	PotentialBuyerID INT FOREIGN KEY REFERENCES dbo.PotentialBuyers(PotentialBuyerID),
	StatusID INT FOREIGN KEY REFERENCES dbo.status(StatusID)
);
GO

CREATE TRIGGER tr_PotentialBuyers_Insert
ON dbo.PotentialBuyers
AFTER INSERT
AS
BEGIN
    INSERT INTO dbo.logs(LogMessage, LogDate, PotentialBuyerID, StatusID)
    SELECT 'New potential buyer added', GETDATE(), i.PotentialBuyerID, i.StatusID
    FROM inserted i;
END;
GO


CREATE TRIGGER tr_PotentialBuyers_Update
ON dbo.PotentialBuyers
AFTER UPDATE
AS
BEGIN
    IF UPDATE(StatusID)
    BEGIN
        INSERT INTO dbo.logs(LogMessage, LogDate, PotentialBuyerID, StatusID)
        SELECT 'Status changed', GETDATE(), i.PotentialBuyerID, i.StatusID
        FROM inserted i
        INNER JOIN deleted d ON i.PotentialBuyerID = d.PotentialBuyerID
        WHERE i.StatusID <> d.StatusID;
    END
END;
GO


INSERT INTO dbo.Makes(MakeName) values ('Toyota'),('Ferrari'),('Lamborghini'),('Rolls Royce');

INSERT INTO dbo.Models(ModelName) values ('Corolla'),('Camry'),('Avalon'),('Supra'),('458 Italia'),('F8 Tributo'),('Aventador'),('Urus'),('Phantom'),('Cullinan');

INSERT INTO dbo.SubModels(SubModelName) values ('LE'),('SE'),('XLE'),('XSE'),('Base'),('Speciale'),('Spider'),('Evo'),('Coupe'),('SUV');

INSERT INTO dbo.Locations(LocationName, ZipCode) values ('Dallas', '75201'),('Houston', '77001'),('Austin', '73301'),('San Antonio', '78201');

INSERT INTO dbo.Costumers(FirstName, LastName, Email, Phone, LocationID) values ('Nafer', 'Hernandez', 'nafer@hotmail.com', '1234567890', 1),('Peter', 'Parker', 'piter@gmail.com', '0987654321', 2);

INSERT INTO dbo.Cars(MakeID, ModelID, SubModelID, LocationID, Year, Price,CostumerID,RegistrationNumber) values (1, 1, 1, 1, 2020, 20000.00,1,'DFF897'),(2, 5, 9, 2, 2020, 200000.00,2,'KJH743'),(3, 7, 7, 3, 2020, 300000.00,1,'434RFF'),(4, 9, 10, 4, 2020, 400000.00,2,'YTR908');

INSERT INTO dbo.status(StatusName) values ('Pending'),('Acceptance'),('Accepted'),('Picked UP');

INSERT INTO dbo.Buyers(FirstName, LastName, Email, Phone, LocationID) values ('John', 'Doe', 'john@hotmail.com', '1234567890', 1),('Jane', 'jaczon', 'jane@gmailo.com', '0987654321', 2),('Jack', 'Doe', 'jack@outlook.com', '6789012345', 3),('Jill', 'Dan', 'jill@hotmail.com', '4567890123', 4);

INSERT INTO dbo.PotentialBuyers(BuyerID, amount, IsCurrentOne, DatePickup, StatusID,CarID) values (1, 20000.00, 0, '2020-01-01', 1,1),(2, 200000.00, 0, '2020-01-01', 1,2),(3, 300000.00, 0, '2020-01-01', 1,3),(4, 400000.00, 0, '2020-01-01', 1,4);

SELECT
	B.FirstName + ' ' +B.LastName AS BuyerName,
	PB.amount AS Amount,
	S.StatusName,
	C.RegistrationNumber,
	M.ModelName,
	MK.MakeName
FROM PotentialBuyers AS PB
INNER JOIN Buyers AS B ON PB.BuyerID = B.BuyerID 
INNER JOIN Status AS S ON PB.StatusID = S.StatusID
INNER JOIN Cars AS C ON PB.CarID = C.CarID
INNER JOIN Models AS M ON C.ModelID = M.ModelID
INNER JOIN Makes AS MK ON C.MakeID = MK.MakeID


CREATE TABLE dbo.Orders
(
	OrderID INT CONSTRAINT pk_Orders_OrderID PRIMARY KEY IDENTITY,
	OrderDate DATE,
	CustomerID INT,
	StatuID INT,
	IsActive BIT
);
GO

INSERT INTO dbo.Orders(OrderDate, CustomerID, StatuID, IsActive) values ('2024-01-01', 1, 1, 0),('2024-06-01', 2, 2, 0),('2024-10-01', 3, 1, 1),('2020-12-01', 4, 1, 1);