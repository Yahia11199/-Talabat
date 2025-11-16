## 🍔 Talabat RESTful API
<p align="center"> <strong>Enterprise-Grade RESTful API for Food Delivery Platform built with ASP.NET Core 9.0 🚀</strong> </p> 
<p align="center"> <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET-9.0-blueviolet?style=for-the-badge&logo=dotnet" /></a> 
  <a href="https://github.com/Yahia11199"><img src="https://img.shields.io/badge/Made%20By-Ahmed%20Ragheb-black?style=for-the-badge&logo=github" /></a> 
</p>

---

## 🧭 Overview

Talabat API is a production-ready backend system designed for managing food delivery operations.
Built with ASP.NET Core 9.0, Clean Architecture, and the Repository + Unit of Work pattern, it delivers modular, scalable, and maintainable code.

The system supports authentication (JWT + Roles + Refresh Tokens), advanced caching, pagination, and audit logging — all optimized for real-world enterprise performance.

---


## ⚡ Highlights

- 🧱 **Clean Architecture & SOLID Principles**
- ⚙️ **Repository + Unit of Work Pattern**
- 🧩 **Dependency Injection Everywhere**
- 🔐 **JWT Authentication & Role-Based Authorization**
- 📬 **Entity Framework Core (SQL Server)**
- 🕓 **Hybird Caching (Memory + Distributed)**
- 📈 **Serilog Logging with Enrichers**
- 💾 **Caching & Performance Optimization**
- 🌍 **CORS & Global Error Handling**
- 🔁 **Refresh Tokens & Account Management**
- 🧩 **Pagination, Filtering & Sorting**
- 🧠 **Application Options & Configuration**
- 🧮 **Audit Logging & Activity Tracking**
- 💬 **Centralized Exception & Problem Details**
- 🧰 **API Versioning & Rate Limiting**
- 📘 **Swagger & Postman**
- ❤️ **Health Checks**
- ☁️ **Cloud Deployment (MonsterASP)**

---

## 🏗️ Architecture Overview

```
Talabat
├── Talabat.Api → (Controllers, Swagger, Middleware)
├── Talabat.Application → (Business Logic, DTOs, Mapping, Validation)
├── Talabat.Infrastructure → (Repositories, Database, Logging, Caching)
└── Talabat.Domain → (Entities, Enums, Core Models)
```

## 📊 System Architecture Diagram
```flowchart LR
    A[Client / Swagger UI] --> B[Talabat.API]
    B --> C[Application Layer]
    C --> D[Infrastructure Layer]
    D --> E[(SQL Server Database)]
    D --> F[(Caching System)]
    D --> G[(Serilog Logging)]
```

> 💡 Shows the flow between the API, business layer, infrastructure, and core services.
---

## 🧱 Folder Structure
```bash
📁 Talabat
 ┣ 📂 Talabat.Api                 # Controllers, Middleware, Swagger, Extensions
 ┣ 📂 Talabat.Application         # Business Logic, DTOs, Mapping, Validation
 ┣ 📂 Talabat.Infrastructure      # Repositories, EF Config, Logging, Caching
 ┣ 📂 Talabat.Domain              # Entities, Enums, Value Objects
 ┗ 📜 README.md                   # Project Documentation
```

> 🧩 This structure reflects **Clean Architecture** and **Repository + Unit of Work Pattern**, separating concerns clearly for scalability and maintainability.

---

## 🧠 Core Development Features

| Feature                          | Description                               |
| -------------------------------- | ----------------------------------------- |
| 🔐 **Authentication**            | Secure login using JWT & Refresh Tokens   |
| 👑 **Authorization**             | Role-Based Access (Admin, Owner, User)    |
| 🧱 **Repository + UoW**          | Clean data layer abstraction              |
| ⚙️ **EF Core Integration**       | Efficient SQL Server ORM                  |
| 💾 **Caching (Hybrid)**          | Memory + Distributed cache layer          |
| 📊 **Pagination & Filtering**    | Optimized data retrieval                  |
| 🧮 **Audit Logging**             | Track user and system activities          |
| 🧩 **Seeding Data**              | Auto-create admin roles and users         |
| 🚦 **Rate Limiting**             | Protect endpoints from abuse              |
| 🩺 **Health Checks**             | Monitor app & dependencies                |
| 🧰 **Global Exception Handling** | Unified error response system             |
| ⚡ **Logging (Serilog)**          | Structured & enriched logs               |
| 🔢 **API Versioning**            | Multi-version endpoint support            |
| 🌐 **CORS Configuration**        | Secure client access control              |

---

## 🧾 Sample API Endpoints

| HTTP | Endpoint                  | Description             | Auth              |
| ---- | ------------------------- | ----------------------- | ----------------- |
| POST | `/api/auth/register`      | Register a new user     | ❌                 |
| POST | `/api/auth/login`         | Login and get JWT       | ❌                 |
| POST | `/api/auth/refresh`       | Refresh access token    | ✅                 |
| GET  | `/api/users`              | Get all users           | 🔒 Admin          |
| POST | `/api/roles`              | Add new role            | 🔒 Admin          |
| GET  | `/api/store`              | List all stores         | ✅                 |
| GET  | `/api/products`           | List menu items         | ✅                 |
| POST | `/api/orders`             | Create new order        | 🔒 User           |
| PUT  | `/api/orders/{id}/status` | Update order status     | 🔒 Admin/Delivery |
| GET  | `/api/orders/history`     | View user order history | 🔒 User           |


---

## ⚙️ Local Setup (Development)

1️⃣ **Clone Repository**
```bash
git clone https://github.com/Yahia11199/-Talabat.git
cd TalabatAPI
```

2️⃣ **Configure Database**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=Talabat;Trusted_Connection=True;"
}
```

3️⃣ **Add Secrets**
```bash
dotnet user-secrets set "Jwt:Key" "YourSuperSecretKey123"
```

4️⃣ **Run the App**
```bash
dotnet run --project Talabat.Api
```

5️⃣ **Open Swagger**
```bash
https://localhost:5001/swagger/index.html
```

---
## 🧮 Caching System
- Uses Hybrid Caching **(In-Memory + Distributed)**
- Improves performance and reduces DB load
- Cache invalidation handled automatically on CRUD operations
---

---
## 📊 Logging & Monitoring

- Integrated with Serilog for structured logs
- Logs stored in files + console output
- Includes request tracing & exception logging
- Can be extended to external providers (Seq, ElasticSearch)
---

---
## 🧠 Audit Loggingg
- Tracks critical operations (Create, Update, Delete)
- Logs user info, IP address, and timestamp
- Ideal for monitoring admin or high-privilege actions
---


## 💡 DevOps & CI/CD

| Tool                   | Purpose                 |
| ---------------------- | ----------------------- |
| **Azure DevOps**       | CI/CD Pipelines         |
| **MonsterASP**         | API Hosting             |
| **GitHub Actions**     | Build & Test Automation |
| **EF Core Migrations** | Database Versioning     |

---

## 📜 License
This project is licensed under the **MIT License** — use, modify, and distribute freely with attribution.
