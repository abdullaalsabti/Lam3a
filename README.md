
# Lam3a – Backend (.NET 9 Web API) Tech Stack

| Tool / Package                   | Purpose / Role                                   | Notes / Why we use it                           |
|----------------------------------|--------------------------------------------------|------------------------------------------------|
| **ASP.NET Core Web API**         | API layer                                        | Handles HTTP requests, REST endpoints           |
| **Entity Framework Core**        | ORM (Code-First)                                 | For database access and migrations              |
| **PostgreSQL**                   | Primary database                                 | Reliable, scalable RDBMS                        |
| **PostgreSQL + PostGIS**         | Geospatial extension                             | Stores and queries driver location history      |
| **Redis (L1 cache)**             | Distributed caching + live driver location       | Fast updates for driver positions               |
| **In-Memory Cache (L2 cache)**   | Local caching                                    | Improves performance and reduces DB calls       |
| **RabbitMQ**                     | Message broker / microservices                   | Enables event-driven architecture               |
| **Firebase Storage**             | Image/file storage                               | Stores car photos, profile images, receipts     |
| **SignalR (WebSockets)**         | Real-time communication                         | Pushes live driver updates to Flutter clients   |
| **FluentValidation**             | Input validation                                 | Declarative validation rules for DTOs           |
| **Bogus**                        | Fake data generation                             | Seeding test/dummy data                         |
| **Rate Limiting Middleware**     | Request throttling                               | Prevents abuse and protects API resources       |
| **Error Handling Middleware**    | Centralized exception handling                   | Unified error responses, logging                |
| **Timeout Middleware**           | Request timeout enforcement                      | Avoids long-running requests                    |
| **xUnit / NUnit + Moq**          | Unit testing                                     | Testing API logic and services                  |
| **Integration Tests**            | API-level tests                                  | Validates real HTTP requests and DB interactions|
