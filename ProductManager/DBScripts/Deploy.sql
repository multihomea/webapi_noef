-- Create a new database called 'ProductsDB'
-- Connect to the 'master' database to run this snippet
USE master
GO

-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'ProductsDB'
)
CREATE DATABASE ProductsDB
GO

-- Switch to new database
USE ProductsDB
GO

BEGIN TRANSACTION;
GO

-- Create table in new database
CREATE TABLE [Products] (
    [ProductId] uniqueidentifier DEFAULT NEWID() NOT NULL,
    [Name] varchar(16) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);
GO

-- Create a new stored procedure called 'AddProduct' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'AddProduct'
)
DROP PROCEDURE dbo.AddProduct
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.AddProduct
    @Name VARCHAR(16),
    @StartDate DATETIME2(7) ,
    @EndDate DATETIME2(7)
AS

BEGIN
    INSERT INTO dbo.Products
        (Name,StartDate,EndDate)
        OUTPUT inserted.ProductId
    VALUES
        (@Name, @StartDate, @EndDate)
END
GO

-- Create a new stored procedure called 'GetProducts' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetProducts'
)
DROP PROCEDURE dbo.GetProducts
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.GetProducts
AS
    SELECT * FROM Products
GO

COMMIT;
GO



