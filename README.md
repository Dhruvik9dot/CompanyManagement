# Company Management Project  

## Overview  

Welcome to my project! This project is designed to demonstrate the functionality of a web API built on the ASP.NET Core framework, featuring basic CRUD operations and API validation. This README will guide you through the setup and operation of the project.  

## Setup Instructions  

### 1. Change the Connection String  

Update the connection string in the `appsettings.json` file to point to your local database. Look for the following section in the file and modify it accordingly:  

`json` 
{   
  "ConnectionString": "Your_Connection_String_Here"  
}  

### 2. Update Database

To create the database based on your updated configurations, open the Package Manager Console in Visual Studio and run the following command:update-database

### 3. User Authentication
For user authentication, use the following credentials:

Username: Admin
Password: Admin@123

Ensure that you configure your API to require these credentials for authorization. Users must authenticate using these credentials before accessing the endpoints.

### 4. API Validation
The following rules apply to the API validation:

Company Name & Email: Both are mandatory fields when creating or updating a company record.
Unique Constraints: The company name and email must be unique across all records. Duplicate entries will not be allowed.

