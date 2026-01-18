# BeymenCaseStudy

# 🛒 Modular Monolith E-Commerce

### 🛠 Quick Start
1. Ensure **Docker Desktop** is running.
2. Run `docker-compose up -d`.
3. The API will wait for Postgres and RabbitMQ to be healthy, then automatically apply migrations and seed initial stock data.

### 🏗 Architecture
- **Modular Monolith**: Organized into Order, Stock, and Notification modules.
- **Database Isolation**: Each module owns its specific schema within PostgreSQL (`orderdb`, `stockdb`, `notificationdb`).
- **Async Messaging**: Modules communicate via RabbitMQ using a Generic Publisher/Consumer pattern.
- **Validation**: Strict input rules using FluentValidation with standardized RFC 7807 error responses.
- **Global Error Handling**: Centralized exception management using the .NET `IExceptionHandler`.
