# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

AlwaysMoveForward is a shared common library ecosystem serving multiple projects (AnotherBlog, PointChart). It includes:
- Common utility libraries (.NET 8)
- Multiple data access layer implementations (NHibernate, Entity Framework, Dapper)
- A main portal site (ASP.NET MVC 5)
- A React/TypeScript landing page
- OAuth services (1.0a and 2.0)

## Build Commands

### AlwaysMoveForward.Common (.NET 8)
```bash
cd Common/AlwaysMoveForward.Common
dotnet build
dotnet build -c Release
```

### Legacy .NET Framework Projects
```bash
# Build the solution (requires Visual Studio 2018+ or msbuild)
msbuild AlwaysMoveForward.Common.sln
```

### LandingPage (React/TypeScript)
```bash
cd LandingPage
npm install
npm run build          # Production build (webpack.production.js)
npm run build-dev      # Development build (webpack.development.js)
npm run watch          # Development with watch mode
```

## Testing

NUnit tests exist in the codebase. Run them through Visual Studio Test Explorer or `dotnet test`.

## Database Setup

SQL Server is required. Run database scripts in order from the Database folder to create schema and seed data. Configure connection strings in `Web.config` and `App.config` files.

## Architecture

### Layered Structure
- **DataLayer**: Repository pattern with `IRepository<T>` and `IUnitOfWork` interfaces
  - Three ORM implementations available: NHibernate, Entity Framework, Dapper
- **Business**: Service classes (e.g., `PollService`, `EmailManager`)
- **DomainModel**: Entities (User, Poll, DbInfo)
- **Configuration**: Centralized config classes (DatabaseConfiguration, EmailConfiguration, AESConfiguration)
  - Configuration sections use `AlwaysMoveForward/` prefix

### Key Directories
```
/src
  /Common/AlwaysMoveForward.Common/    # Main shared library (.NET 8)
    /Business/                          # Service layer
    /Configuration/                     # Config classes
    /DataLayer/                         # Repository interfaces
    /DomainModel/                       # Entity classes
    /Encryption/                        # AES, RSA, hashing utilities
    /Security/                          # Auth helpers
  /Common/DataLayer.*/                  # ORM-specific implementations
  /MainSite/                            # ASP.NET MVC 5 portal
  /LandingPage/                         # React/TypeScript frontend
```

### Security/Encryption
The codebase includes significant encryption infrastructure:
- AES encryption (AESManager using `Aes.Create()` and `Rfc2898DeriveBytes`)
- RSA helpers (using `GetRSAPublicKey()`/`GetRSAPrivateKey()`)
- X509 certificate support
- MD5/SHA1 hashing utilities

## Technology Stack

**Backend**: .NET 8 (AlwaysMoveForward.Common), ASP.NET MVC 5.2.3, NHibernate, Entity Framework, Dapper, log4net 2.0.17, AutoMapper 13.0.1

**Frontend**: React 17, Redux, React Router v6, TypeScript 5, Webpack 5, Bootstrap 3, jQuery

**Database**: SQL Server

## Deployment

- MainSite: IIS Express locally at `http://localhost:61007/`
- OAuth2 projects: Docker/Kubernetes deployment to AWS (see Jenkinsfile.txt)
- Other projects: Manual deployment

## Current State

Branch `UpgradeProjects` indicates active modernization work. Project namespaces now use `AlwaysMoveForward.*`. The core common library has been upgraded to .NET 8.
