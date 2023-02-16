# Workout Masters API

A RESTful API for a fitness tracking application built using the .NET 6 framework. The API allows users to register and authenticate users, create and manage their workouts and store data in the database.
This API uses ASP.NET Core Identity. ASP.NET Core Identity is the membership system for building ASP.NET Core web applications, including membership, login, and user data. It allows you to manage users, passwords, profile data, roles, claims, tokens, email confirmation, and more.

## Getting Started

These instructions will help you set up the Workout Masters API on your local machine for development and testing purposes.

## Prerequisites

- .NET 6 SDK
- Visual Studio 2021 or later
- SQL Server

## Installation

1. Clone the repository to your local machine

```sh
git clone https://github.com/nnd1402/workout-masters-api.git
```

2. Open the solution file in Visual Studio
3. Build the solution to restore the NuGet packages and dependencies
4. Create the application.json file in WorkoutMasters.API project
5. Choose your prefered solution to store data:

   - To use the in-memory database configuration set the database type in appsettings.json file by adding:

   ```sh
   "DatabaseType": "Memory"
   ```

   or

   - To use your local SqlServer database: set the database type in appsettings.json file by adding:

   ```sh
   "DatabaseType": "SQL"
   ```

   and add the connection string in the appsettings.json file to point to your local SQL Server instance

   - Connection string example

   ```sh
   "ConnectionStrings": {
    "SqlServer":"Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword"};
   ```

6. Add prefered token key string in appsettings.json to register your JWT Token signature
   - Token key example:
   ```sh
   "TokenKey": "Your token key"
   ```
7. Create new folder Properties in WorkoutMasters.Domain project and create new Resources.resx file in it
8. Provide your prefered email host, address and password for configuring information for email sending service. Set this in your Resources.resx file you just created.
   - Email information example:
   ```sh
   "EmailHost": "Your email host",
   "EmailUsername": "Your email address",
   "EmailPassword": "Your email password"
   ```
9. Run the API by running the WorkoutMasters.API as your default project. This will start the application on your local machine. Navigate to https://localhost:7116/swagger to access the Swagger UI

## Configuration

The API can be configured by modifying the settings in the appsettings.json file.

## Usage

The API can be used to perform CRUD operations on workout plans and related data, as well as manage user authentication and registration.

You can test the API by making HTTP requests to the endpoints or by using the Swagger UI.

## Authors

Nenad Mihajlovic - Initial work

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.
