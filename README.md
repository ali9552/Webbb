# ASP.NET MVC Enterprise Website

A robust enterprise-level web application built using ASP.NET MVC with a clean three-layer architecture, implementing best practices and modern development patterns.

## 🏗️ Architecture

The project follows a three-layer architecture pattern:
- **PL (Presentation Layer)**: MVC web application handling user interface and requests
- **BLL (Business Logic Layer)**: Contains business logic and services
- **DAL (Data Access Layer)**: Handles database operations and data models

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core 9.0
- **ORM**: Entity Framework Core 9.0
- **Authentication**: ASP.NET Core Identity
- **Object Mapping**: AutoMapper 14.0
- **Database**: SQL Server
- **Frontend**: 
  - ASP.NET MVC Views
  - HTML/CSS
  - JavaScript
  - Bootstrap (for responsive design)

## ✨ Features

### 1. User Management
- User registration and authentication
- Role-based authorization
- User profile management
- Password recovery and reset

### 2. Employee Management
- Create, Read, Update, and Delete (CRUD) operations for employees
- Employee profile management
- File/Image upload for employee documents
- Employee search and filtering

### 3. Department Management
- Department CRUD operations
- Department-Employee relationship management
- Department hierarchy management

### 4. Role Management
- Create and manage user roles
- Assign/remove roles from users
- Permission management
- Role-based access control

### 5. Security Features
- Secure authentication using ASP.NET Identity
- HTTPS enforcement
- Token-based authentication
- Password hashing and security

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022 or later

### Setup
1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run Entity Framework migrations
4. Build and run the application

## 🏗️ Project Structure

```
├── website.pl (Presentation Layer)
│   ├── Controllers/
│   ├── Views/
│   ├── Models/
│   ├── wwwroot/
│   └── Program.cs
├── website.bll (Business Logic Layer)
│   ├── Interfaces/
│   ├── Services/
│   └── Repositories/
└── website.dal (Data Access Layer)
    ├── Context/
    ├── Models/
    └── Configurations/
```

## 🤝 Contributing

Feel free to submit issues and enhancement requests.


