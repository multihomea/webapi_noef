USE master;
IF EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'ProductsDB'
)
ALTER DATABASE [ProductsDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [ProductsDB] ;
