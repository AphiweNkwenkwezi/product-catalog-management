# Product Catalog Management System

This project implements a full-stack Product Catalog Management System
using:

-   Backend: ASP.NET Core (.NET 9)
-   Frontend: Angular 21 (Standalone Components)
-   Database: EF Core
-   EF Core category repository 
-   Custom in-memory product repository & product search engine 

------------------------------------------------------------------------

## Tech Stack

### Backend

-   .NET 9
-   ASP.NET Core Web API
-   Entity Framework Core (InMemory)
-   Custom Repository`<T>`{=html} Pattern
-   Custom Request Logging Middleware
-   Dictionary-based Caching Layer
-   Custom JSON Serialization
-   Nullable Reference Types Enabled

### Frontend

-   Angular 21 (Standalone Components)
-   TypeScript Interfaces
-   Reactive Forms
-   RxJS
-   Unit Testing

------------------------------------------------------------------------

# Running the Application

## Backend (.NET 9)

1.  Navigate to backend folder

2.  Restore packages:

    dotnet restore

3.  Run the API:

    dotnet run

------------------------------------------------------------------------

## Frontend (Angular)

1.  Navigate to frontend folder

2.  Install dependencies:

    npm install

3.  Run application:

    ng serve

4.  Open browser:

    http://localhost:4200

------------------------------------------------------------------------

# Backend Features Implemented

-   Generic Repository`<T>`{=html} base class
-   One pure in-memory product repository (List/Dictionary based)
-   Custom LINQ FilterByCategory(categoryIds) and Paginate(products, page, pageSize) extension methods for filtering
-   Record types for DTOs
-   Pattern matching for request validation
-   Nullable reference types enabled
-   Custom request logging middleware (no external libraries used)
-   Dictionary-based caching layer for search
-   Hierarchical category tree builder
-   IComparable implementation for Product sorting
-   Manual model binding
-   Custom JSON serialization endpoint
-   Proper DI registration of ProductSearchEngine, Services and Repos
-   Fuzzy search with weighted scoring (no external libraries used)

------------------------------------------------------------------------

# Data Models

## Product

-   Id
-   Name
-   Description
-   SKU
-   Price
-   Quantity
-   CategoryId
-   CreatedAt
-   UpdatedAt

## Category

-   Id
-   Name
-   Description
-   ParentCategoryId (nullable)

------------------------------------------------------------------------

# API Endpoints

GET /api/products\
GET /api/products/{id}\
POST /api/products\
PUT /api/products/{id}\
DELETE /api/products/{id}

GET /api/categories\
GET /api/categories/tree\
POST /api/categories

Supports pagination, filtering, and search.

------------------------------------------------------------------------

# Testing

Backend:

    dotnet test

Frontend:

    ng test

------------------------------------------------------------------------

# Video Walkthrough

The solution is demonstrated in three short focused walkthrough videos:

1. **Functional Demo**  
   Covers product management, search, category filtering, and API behavior.  
   https://www.loom.com/share/db599b2e60bb4e8a8d80a3c97fde6b7a

2. **Backend Architecture & Search Engine**  
   Covers Clean Architecture, Repository pattern, in-memory search engine, caching, and middleware.  
   https://www.loom.com/share/50ae5f97d0cb4e8b9e0afd443a8d8937

3. **Angular Frontend Overview**  
   Covers standalone components, reactive forms, RxJS, and testing.  
   https://www.loom.com/share/44755a0be89b4bd4a2869da35ad4d48a

