# ASP.NET Core Web API

This is an ASP.NET Core Web API project that allows you to perform CRUD operations on data. The project uses Entity Framework Core for database interactions and Swagger for API documentation and testing.

## Prerequisites

Make sure you have the following installed:

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download)
- A compatible database (e.g., SQL Server)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-repository-url.git
cd your-repository-folder
```

### 2. Configure the Connection String

You need to set up the connection string to your database in the appsettings.json file located in the root of the project.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server-name;Database=your-database-name;User Id=your-username;Password=your-password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

- Replace your-server-name with your database server name (e.g., localhost or 127.0.0.1).
- Replace your-database-name with the name of your database.
- Replace your-username and your-password with the credentials you use to connect to the database.

### 3. Build the Application

To build the project, run the following command in the project root directory:

```bash
dotnet build
```

This command will restore dependencies, build the project, and prepare it for running.

### 4. Apply Migrations (Using Entity Framework Core)

This project uses Entity Framework Core, you need to apply migrations to set up the database.

```bash
cd ./api
```

If you don't have dotnet entity framework tool for CLI integrations, run the following command in your Terminal

```bash
dotnet tool install --global dotnet-ef
```

After having dotnet EF tool installed, run the following command:

```bash
dotnet ef database update
```

This command will apply the existing migrations and create the necessary database tables.

### 5. Run the Application

Once the application is built, you can run it using the following command:

```bash
dotnet run

or

dotnet build
```

This will start the API server. By default, the API will be hosted at ==https://localhost:7179== (HTTPS) or ==http://localhost:5202== (HTTP).

## Swagger API Documentation

This project includes Swagger for API documentation and testing. Swagger provides an interactive interface where you can explore and test the available API endpoints.

### Access Swagger UI

```bash
http://localhost:5202/swagger
```

You will see the Swagger UI, which displays all available API endpoints and allows you to send requests directly from the browser.

### How to Use Swagger

1. Open ==http://localhost:5202/swagger== in your browser.
2. Explore the available API endpoints in the Swagger UI.
3. Click on any endpoint to expand its details.
4. Use the built-in forms to input data and test the API by clicking the **Try it out** button.

## Additional Commands

- To clean the build artifacts

```bash
dotnet clean
```
