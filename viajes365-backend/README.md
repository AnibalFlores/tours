
# Rest Api Viajes 365º 

ASP .NET Core 5.0 - REST API for Viajes 365 Content Managment System, Authentication Registration and Forum

GitHub Repository: https://github.com/classroomEntreRios/proyecto-g1b-proyecto-g1b

## Pre-Requisites

  - Lastest community [Visual Studio 2019](https://visualstudio.microsoft.com/es/vs/community/) (its free and you must register to get a personal key)
  - [Net Core 5 SDK](https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.200-windows-x64-installer) 
  - [SQL SERVER EXPRESS 2019](https://www.microsoft.com/es-ar/download/details.aspx?id=101064)
  - Lastest SSMS ([Sql Server Managment Studio (in spanish)](https://go.microsoft.com/fwlink/?linkid=2151644&clcid=0x40a))
  - SSD drive, 4 cores, 8 Gb or more memory and Windows 10 (better if patched almost 2020)

## For the SQL Server connections strings...
Can be used ***Windows Authentication*** or ***better enable TCP with Server Logins:***  
 - First: with ***SQL MANAGER*** Enable TCP/IP and open port **1433** under **"IP Addresses"** tab **"IPALL"** 
- Second:  with ***SSMS*** create the new user under Security->Logins  with server roles **public** and **sysadmin** *(Example, user: admin password: Abcd#1234)*

**Important**: In server properties->Security->Server Authentication set option **"SQL Server and Windows Authentication mode"**

check **"ConnectionStrings"** sections in ***appsettings.json*** and ***appsettings.Development.json***
files for example 

    "Viajes365Database": "Data Source=tcp:127.0.0.1,1433;Initial Catalog=fullstack2;User ID=**tuusuario**;Password=**tuclave**;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

**Important**: Tip ***Initial Catalog*** refers to the database name for the *InitialCreate* migration.
   
## Migrations
From console or VS2019 Powershell for developers

First: Install the tool (CLI) 

    dotnet tool install --global dotnet-ef

Second: Update the tool if is already installed

    dotnet tool update --global dotnet-ef

**Actual DataContext (SQL Server Express)**

Build an Initial migration (delete folder **SqlServerMigrations** if exists)

    dotnet ef migrations add InitialCreate -o Migrations/SqlServerMigrations

drop database 

    dotnet ef database drop

update database 

    dotnet ef database update

Regards!
Aníbal.-

***Notes***: 
- Make a shortcut for SQL Manager if refuses to start! the path in Win 10 64bits is

     C:\Windows\SysWOW64\SQLServerManager15.msc

- Database update creates DB but users **admin** and **user** are created at runtime if not exists