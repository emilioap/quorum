# Quorum API

## 📌 Project Overview
Quorum API is a .NET 8-based Web API that provides functionalities to manage **legislators, bills, and votes**. The API allows users to retrieve data such as:
- The number of legislators who supported or opposed a bill.
- The primary sponsor of a bill.
- The number of bills a legislator has supported or opposed.

## 🏗️ Tech Stack
- **.NET 8** – Backend framework
- **ASP.NET Core Web API** – API development
- **C#** – Main programming language
- **Moq & xUnit** – Unit testing
- **Microsoft.Extensions.Caching.Memory** – In-memory caching
- **Swagger (Swashbuckle)** – API documentation
- **Dependency Injection (DI)** – Service management

## 🏛️ Project Architecture
The project follows a **Clean Architecture** approach, structured into the following layers:

```
quorum.webapi        -> API Layer (Controllers & Routes)
quorum.service       -> Business Logic Layer (Services & Business Rules)
quorum.domain        -> Core Entities & Interfaces (Models & Contracts)
quorum.infrastructure -> Data Handling (Repositories & Persistence - if needed)
quorum.tests         -> Unit Tests (Moq & xUnit for testing services)
```

## ⚙️ Features
- Retrieve the number of supporters and opponents for a bill.
- Fetch the primary sponsor of a bill.
- Get the number of bills a legislator has supported or opposed.
- **Swagger Documentation** for API endpoints.
- **In-Memory Caching** for optimized response times.
- **Proper Exception Handling & HTTP Status Codes**.

## 🚀 Getting Started

### 1️⃣ Prerequisites
- **.NET 8 SDK** installed ([Download Here](https://dotnet.microsoft.com/download))
- **Visual Studio / VS Code / Rider**

### 2️⃣ Clone the Repository
```sh
 git clone https://github.com/emilioap/quorum.git
 cd quorum
```

### 3️⃣ Build and Run the Application
```sh
 dotnet build
 dotnet run --project quorum.webapi
```

### 4️⃣ Access API Documentation
Once the application is running, open **Swagger UI** at:
- 📌 `http://localhost:5000/swagger`

## 🔥 Running Tests
To run unit tests:
```sh
 dotnet test
```
