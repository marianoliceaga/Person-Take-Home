# Person API – CQRS Implementation

This is a production-grade ASP.NET Core Web API for managing "Person" entities using the **CQRS pattern**, **Domain-Driven Design (DDD)**, and **clean architecture principles**.

---

## 🚀 How to Run the Project

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 or Rider (optional, but recommended)
- cURL, Postman, or any API testing tool

### Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/marianoliceaga/Person-Take-Home
   cd PersonAPI
   ```

2. **Run the application**

   ```bash
   dotnet run --project src/PersonAPI.WebApi
   ```

   > 🔐 All requests must include the custom header:  
   > `x-client-id: PersonAPI-Client-2024`

3. **Access the Swagger UI**

   Visit [http://localhost:5000/swagger](http://localhost:5000/swagger) or wherever the app is hosted.

4. **Health check**

   ```
   GET /health
   ```

---

## 📌 Assumptions

- Gender values are limited to: `"Male"`, `"Female"`, `"Other"`, and `"PreferNotToSay"`.
- Person entities are **immutable** except through approved commands (e.g., `RecordBirthCommand`, `RecordDeathCommand`).
- Client identification is required for all endpoints using a static header `x-client-id`.
- The application uses an **in-memory database** (`UseInMemoryDatabase`) for demo/testing purposes.
- Versioning is handled manually and persisted via a `PersonVersion` entity.

---

## 🧠 Challenges Faced & Solutions

| Challenge | Solution |
|----------|----------|
| **Command/query separation** complexity | Used **MediatR** to cleanly separate write/read concerns |
| Maintaining data consistency with value objects | Implemented immutable **ValueObjects** and EF Core `OwnsOne` mappings |
| Validating client headers globally | Created `ClientIdValidationMiddleware` that filters requests before controller actions |
| Exception handling across layers | Built a centralized `ExceptionHandlingMiddleware` with mapped responses and logging |
| Date handling (e.g., DateOnly support in .NET) | Ensured `DateOnly` works correctly in APIs with custom JSON converters and validation |
| Providing meaningful audit/version history | Implemented a full **versioning system** for the `Person` entity |
| Input validation | Integrated **FluentValidation** with MediatR `IPipelineBehavior` to handle validation cross-cutting concerns |

---

## 💡 Future Improvements

If given more time, the following improvements would be planned:

### 🔧 Infrastructure
- Switch from in-memory database to **SQL Server** or **PostgreSQL**.
- Add **Dockerfile** and `docker-compose` for containerization.
- Enable **Entity Framework Migrations** support.

### 🔒 Security
- Replace static `x-client-id` with proper **JWT Authentication**.
- Implement **rate limiting** and request throttling.

### 🧪 Testing
- Add **mock integration tests** with a real database.
- Implement **test coverage reports** and **CI/CD pipeline**.

### 🧭 Features
- Support `RecordDeathCommand`.
- Add filtering/search (e.g., by name, gender, or location).
- Extend versioning to include detailed change logs.
- Create **OpenTelemetry support** for distributed tracing.

---

## 🛠 Tech Stack

- ASP.NET Core 8
- CQRS with MediatR
- Entity Framework Core
- Serilog (structured logging)
- FluentValidation
- AutoMapper
- Swagger / OpenAPI
- xUnit / Moq (unit & integration tests)

---

## 📬 Contact

For questions or improvements, please contact the maintainer or open an issue.
