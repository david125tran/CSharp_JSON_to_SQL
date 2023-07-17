# CSharp_to_SQL_dapper  
This project uses the dapper mapper (an open-source object-relational mapping (ORM)  
library for .NET and .NET Core applications). I parse JSON data into string and then  
use the dapper library to inject it into a MS SQL table as TSQL code.  
  
Step 1: Start by opening the "startSql.sql" file in Azure Data Studios.  Run this file 1x.  
![Azure Data Studios](https://github.com/david125tran/CSharp_to_SQL_dapper/blob/main/Images/1.png)

Step 2: Open Windows Powershell.  From there, change directorys to the package location.  
Run the rest of the package in Windows Powershell by using "dotnet run".  
From there, the JSON data will be mapped into the MS SQL database.  
You should get a prompt if the SQL table injection is successful.  
![Windows Powershell](https://github.com/david125tran/CSharp_to_SQL_dapper/blob/main/Images/2.png)
