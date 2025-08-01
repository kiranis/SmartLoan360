# SmartLoan360

A comprehensive microservices-based loan processing platform showcasing modern architecture patterns with .NET 8, Angular, Python, and containerization.

## Architecture Overview

SmartLoan360 demonstrates a production-ready microservices architecture with clean separation of concerns:

- **Loan API** (.NET 8) - Core business logic and loan processing
- **Scoring Engine** (Python/FastAPI) - Risk assessment and loan scoring algorithms  
- **Web UI** (Angular) - Modern responsive frontend interface
- **Authentication** - JWT-based security with role-based access control

## Features

### Current Implementation
- **JWT Authentication** - Secure token-based authentication
- **Loan Application Processing** - Complete loan workflow
- **Risk Scoring Engine** - Python-based scoring algorithms
- **RESTful APIs** - Well-documented API endpoints
- **Swagger Documentation** - Interactive API documentation
- **CORS Support** - Cross-origin resource sharing
- **Input Validation** - FluentValidation for robust data validation
- **CQRS Pattern** - Command/Query separation with MediatR
- **Responsive UI** - Modern Angular frontend

### Architecture Patterns
- **Microservices Architecture** - Loosely coupled, independently deployable services
- **CQRS (Command Query Responsibility Segregation)** - Separation of read/write operations
- **Domain-Driven Design** - Feature-based organization
- **Dependency Injection** - Loose coupling and testability
- **Repository Pattern** - Data access abstraction

## Technology Stack

### Backend Services
- **.NET 8** - Modern C# web API framework
- **MediatR** - CQRS and mediator pattern implementation
- **FluentValidation** - Powerful validation library
- **JWT Bearer Authentication** - Secure token-based auth
- **Swagger/OpenAPI** - API documentation and testing

### Scoring Engine
- **Python 3.x** - High-performance scripting language
- **FastAPI** - Modern, fast web framework for building APIs
- **Pydantic** - Data validation using Python type annotations
- **Uvicorn** - Lightning-fast ASGI server

### Frontend
- **Angular 16+** - Modern TypeScript-based framework
- **RxJS** - Reactive programming with observables
- **HTTP Interceptors** - Automated token management
- **Responsive Design** - Mobile-first UI/UX

## Project Structure

```
SmartLoan360/
├── src/
│   ├── loan-api/                 # .NET 8 Web API
│   │   ├── Features/            # Feature-based organization
│   │   │   ├── ApplyLoan/       # Loan application feature
│   │   │   └── ScoreLoan/       # Loan scoring feature
│   │   ├── Extensions/          # Service configuration
│   │   └── Program.cs           # Application entry point
│   │
│   ├── scoring-engine/          # Python FastAPI service
│   │   ├── main.py             # FastAPI application
│   │   └── requirements.txt    # Python dependencies
│   │
│   └── web-ui/                 # Angular frontend
│       ├── src/app/
│       │   ├── pages/          # Application pages
│       │   ├── services/       # Angular services
│       │   └── app.module.ts   # Main module
│       └── angular.json        # Angular configuration
│
├── tests/                      # Test projects
│   └── loan-api-tests/         # Integration tests
├── docker-compose.yml          # Multi-container orchestration
└── azure-pipelines.yml         # CI/CD configuration
```

## Getting Started

### Prerequisites
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** - [Download](https://nodejs.org/)
- **Python 3.8+** - [Download](https://python.org/downloads/)
- **Angular CLI** - `npm install -g @angular/cli`

### Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/SmartLoan360.git
   cd SmartLoan360
   ```

2. **Start the .NET API**
   ```bash
   cd src/loan-api
   dotnet run
   ```
   API will be available at `https://localhost:51620/swagger`

3. **Start the Python Scoring Engine**
   ```bash
   cd src/scoring-engine
   pip install -r requirements.txt
   python main.py
   ```
   Service will be available at `http://localhost:8000/docs`

4. **Start the Angular Frontend**
   ```bash
   cd src/web-ui
   npm install
   npm start
   ```
   Application will be available at `http://localhost:4200`

### Default Credentials
- **Username**: `admin`
- **Password**: `password`

## API Documentation

### Authentication Endpoints
- `POST /api/token` - Get JWT authentication token

### Loan Processing Endpoints
- `POST /api/apply` - Submit loan application (requires auth)
- `POST /api/score` - Get loan risk score (requires auth)

### Scoring Engine Endpoints
- `POST /score` - Calculate loan risk score

Access interactive API documentation:
- **Loan API**: `https://localhost:51620/swagger`
- **Scoring Engine**: `http://localhost:8000/docs`

## Testing

### API Testing with Swagger
1. Navigate to `https://localhost:51620/swagger`
2. Obtain token from `/api/token` endpoint
3. Click **Authorize** and enter: `Bearer {your-token}`
4. Test protected endpoints

### Sample Request
```json
{
  "fullName": "John Doe",
  "amount": 10000,
  "termMonths": 12
}
```

## Upcoming Features

### Testing & Quality
- **Unit Tests** - Comprehensive test coverage for all services
- **Integration Tests** - End-to-end testing scenarios
- **Test Automation** - Automated testing in CI/CD pipeline
- **Code Coverage** - High code coverage target

### Containerization
- **Docker Support** - Complete containerization of all services
- **Docker Compose** - Multi-container orchestration
- **Container Registry** - Automated image building and publishing
- **Environment Configuration** - Container-based configuration management

### DevOps & Deployment
- **CI/CD Pipelines** - Azure DevOps/GitHub Actions integration
- **Automated Deployment** - Staging and production environments
- **Infrastructure as Code** - Terraform/ARM template provisioning
- **Monitoring & Logging** - Application insights and health checks

### Advanced Features
- **Database Integration** - Entity Framework Core with SQL Server
- **Message Queuing** - Azure Service Bus/RabbitMQ integration
- **Caching Layer** - Redis caching implementation
- **API Versioning** - Backward-compatible API evolution
- **Security Enhancements** - OAuth 2.0, rate limiting, API key management

## Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

### Development Workflow
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- **Documentation**: Check our [Wiki](https://github.com/yourusername/SmartLoan360/wiki)
- **Issues**: Report bugs via [GitHub Issues](https://github.com/yourusername/SmartLoan360/issues)
- **Discussions**: Join our [GitHub Discussions](https://github.com/yourusername/SmartLoan360/discussions)

## Show Your Support

If you find this project helpful, please consider giving it a star on GitHub!

---

**SmartLoan360** - Demonstrating modern microservices architecture with real-world loan
