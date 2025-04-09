# Shop API

## Overview
The **Shop API** is a RESTful web application built using **ASP.NET Core** targeting **.NET 8**. It provides functionality for user authentication, item management, and order processing. The API uses **JWT (JSON Web Tokens)** for secure authentication and integrates with **Swagger** for API documentation and testing.

---

## Features
- **User Authentication**:
  - User registration and login using **ASP.NET Identity**.
  - JWT-based authentication for secure API access.
- **Item Management**:
  - CRUD operations for items and categories.
- **Order Management**:
  - Manage orders and order items.
- **Swagger Integration**:
  - Interactive API documentation with JWT authentication support.

---

## Technologies Used
- **.NET 8**
- **Entity Framework Core** with SQL Server
- **ASP.NET Identity**
- **JWT Authentication**
- **Swagger (Swashbuckle.AspNetCore)**

---

## Prerequisites
- **.NET SDK 8.0** or later
- **SQL Server** for database
- A valid connection string for the database in `appsettings.json`.

---

## Setup Instructions

1. **Clone the Repository**:
   
```shell
   git clone <repository-url>
   cd Shop
   
```

2. **Configure the Database**:
   - Update the connection string in `appsettings.json`:
     
```json
     "ConnectionStrings": {
       "myCon": "Your SQL Server connection string here"
     }
     
```

3. **Run Migrations**:
   - Apply the database migrations to set up the schema:
     
```shell
     dotnet ef database update
     
```

4. **Run the Application**:
   - Start the application:
     
```shell
     dotnet run
     
```
   - The API will be available at `https://localhost:<port>`.

5. **Access Swagger**:
   - Navigate to `https://localhost:<port>/swagger` to explore and test the API.

---

## Key Endpoints

### Authentication
- **Register**: `POST /api/Account/Register`
  - Registers a new user.
- **Login**: `POST /api/Account/Login`
  - Authenticates a user and returns a JWT token.

### Items
- **Get Items**: `GET /api/Items`
- **Create Item**: `POST /api/Items`

### Orders
- **Get Orders**: `GET /api/Orders`
- **Create Order**: `POST /api/Orders`

---

## JWT Authentication
To access protected endpoints:
1. Obtain a JWT token by logging in via the `/api/Account/Login` endpoint.
2. Include the token in the `Authorization` header of your requests:
   
```
   Authorization: Bearer <your-token>
   
```

---

## Project Structure
- **Controllers**: Handles API endpoints (e.g., `AccountController`).
- **Data**: Contains database models and `AppDbContext`.
- **Extensions**: Custom service extensions (e.g., `CustomJwtAuthExtension`).
- **Migrations**: Database migration files.

---

## Dependencies
- **Microsoft.AspNetCore.Authentication.JwtBearer**
- **Microsoft.EntityFrameworkCore.SqlServer**
- **Swashbuckle.AspNetCore**
