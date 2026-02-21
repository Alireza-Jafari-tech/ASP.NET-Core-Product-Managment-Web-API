# 🛒 Product Management API (ASP.NET Core Web API)

A simple **CRUD (Create, Read, Update, Delete) RESTful API** built with **ASP.NET Core Web API** and **Entity Framework Core**.
This project is part of my learning journey into backend development with ASP.NET Core, focusing on clean architecture, data modeling, filtering, pagination, and service-based design.

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-green.svg)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red.svg)](https://www.microsoft.com/sql-server)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A learning project focused on **ASP.NET Core Web API + Entity Framework Core + SQL Server**.
It demonstrates how to build a structured API with Controllers, Services, Models, filtering, and pagination.


## 🚀 Features

* CRUD operations for **Products**
* Create operation for **Categories**
* Pagination support for product listing
* Filtering products using query parameters
* Separation of concerns using **Services layer**
* Entity Framework Core with **Code-First Migrations**


## 🛠️ Technologies

* **ASP.NET Core 8.0 Web API**
* **Entity Framework Core** (Code-First + Migrations)
* **SQL Server**
* **Swagger / OpenAPI** (for API testing)
* **Visual Studio 2026**


## ⚙️ Installation & Setup

1. Clone the repository:

```bash
git clone https://github.com/Alireza-Jafari-tech/ASP.NET-Core-Product-Managment-Web-API.git
cd ASP.NET-Core-Product-Managment-Web-API
```

2. Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
```

3. Apply migrations and create the database:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

5. Open Swagger in browser:

```text
http://localhost:5164/swagger
```

(or the port shown in your console)


## 📂 Project Structure

```pgsql
/Controllers
  CategoryController.cs   → API endpoints for category CRUD operations
  ProductController.cs    → API endpoints for product CRUD operations

/Data
  AppDbContext.cs         → Entity Framework Core DbContext configuration

/Models
  Product.cs              → Product entity
  Category.cs             → Category entity
  PaginationParams.cs     → Pagination parameters (page number, page size)
  ProductFilter.cs        → Filtering logic for products (query-based filtering)

/Services
  ProductService.cs       → Business logic for products
  CategoryService.cs      → Business logic for categories

/Migrations
  → EF Core migration files for database versioning

Program.cs
  → Application startup configuration (services, middleware, routing)

appsettings.json
  → Database connection string and app configuration

testApi.http
  → HTTP request samples for testing API endpoints
```

 
## 🧑‍💻 Usage

You can test the API using:

* **Swagger UI**
* **Postman**
* Any REST client

### Example Endpoints

#### **Products**

* `GET /api/Product`
  → Get all products with pagination (request body uses `PaginationParams`)

* `GET /api/Product/{id}`
  → Get product by id

* `GET /api/Product/byCategory?categoryId=1`
  → Get products by category id

* `POST /api/Product/filter`
  → Filter products using `ProductFilter`

* `POST /api/Product/search?searchTerm=phone`
  → Search products by name

* `POST /api/Product/Add`
  → Create a new product

* `PUT /api/Product/update?productId=1`
  → Update an existing product

* `DELETE /api/Product/delete?productId=1`
  → Delete a product

#### **Categories**

* `GET /api/Category`
  → Get all categories

* `POST /api/Category/add`
  → Create a new category


![Screenshot 2026-02-21 at 19-55-23 Swagger UI](https://github.com/user-attachments/assets/0551e879-2932-4eda-a2dd-ac774b6a5ad9)

 
## 🎯 Learning Goals

* Understand ASP.NET Core Web API project structure
* Practice CRUD operations with Entity Framework Core
* Implement Service Layer pattern
* Work with pagination and filtering
* Learn database migrations
* Build clean and maintainable API code
* Practice RESTful API principles

 
## 🧪 Testing

The project includes a `testApi.http` file that allows you to test endpoints directly inside Visual Studio / VS Code using the built-in HTTP client.

Example:

```http
GET https://localhost:5164/api/products
```


## 📝 License

This project is licensed under the MIT License.
See the `LICENSE` file for details.


## 🤝 Contributing

This project is for learning purposes, but feel free to fork and improve it.
Suggestions and pull requests are welcome.

Just say: **“add swagger screenshots section”** or **“polish it for GitHub”** 💪
