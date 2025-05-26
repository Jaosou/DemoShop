
## Update Database
Scaffold-DbContext "Server=.\SQLEXPRESS;Database=DemoShop;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\db -Force

## Create Database
use DemoShop

go

CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,    
    LastName VARCHAR(100) NOT NULL,   
    Email VARCHAR(255),                      
    PhoneNumber VARCHAR(15),                
    Address VARCHAR(255),                   
    RegistrationDate DATETIME                
);
CREATE TABLE Sale (
    SaleID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    SaleDate DATETIME NOT NULL,                   
    TotalAmount FLOAT NOT NULL,          
    CustomerID INT NOT NULL,                      
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)   
);

CREATE TABLE SaleBook (
    SaleBookID INT PRIMARY KEY NOT NULL, 
    SaleID INT NOT NULL,                
    BookID nvarchar(50) NOT NULL,        
    Quantity INT NOT NULL,              
    Price FLOAT NOT NULL,                
    FOREIGN KEY (SaleID) REFERENCES Sale(SaleID),  
    FOREIGN KEY (BookID) REFERENCES Book(BookID)   
);

## Insatll Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.VisualStudio.Web.CodeGeneration.Design