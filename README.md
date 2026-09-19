# AppCore

A layered .NET solution that serves as a reusable **application core / starter template**. It separates data access, business logic, models and authentication into their own projects, and exposes them through three interchangeable entry points: a **Web API**, a **Web app**, and a **Windows desktop app**.

<!-- TODO: add 1–2 sentences on the real purpose (e.g. "internal starter for our ERP tools", "base for personal projects"). -->

---

## Table of Contents

- [Solution Structure](#solution-structure)
- [Architecture](#architecture)
- [Projects](#projects)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Conventions](#conventions)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

---

## Solution Structure

```
AppCore/
├── AppCore.slnx
│
├── EndPoints/
│   ├── AppCore.Api      # HTTP API (entry point)
│   ├── AppCore.Web      # Web UI (entry point)
│   └── AppCore.Win      # Windows desktop client (entry point)
│
├── AppCore.Auth         # Authentication & authorization
├── AppCore.BLL          # Business Logic Layer
├── AppCore.DAL          # Data Access Layer
└── AppCore.Model        # Entities / DTOs / shared models
```

The solution uses the new **`.slnx`** solution format, so you need a recent Visual Studio (2022 17.13+) or the .NET SDK 9.0.200+ to open and build it from the CLI.

---

## Architecture

Classic N-layer architecture. Every entry point talks to the same core, so business rules are written once and reused across Web, API and Desktop.

```
 ┌───────────┐  ┌───────────┐  ┌───────────┐
 │ AppCore.  │  │ AppCore.  │  │ AppCore.  │     EndPoints
 │   Api     │  │   Web     │  │   Win     │
 └─────┬─────┘  └─────┬─────┘  └─────┬─────┘
       │              │              │
       └──────────────┼──────────────┘
                      ▼
              ┌───────────────┐      ┌───────────────┐
              │  AppCore.BLL  │◄────►│ AppCore.Auth  │
              └───────┬───────┘      └───────────────┘
                      ▼
              ┌───────────────┐
              │  AppCore.DAL  │
              └───────┬───────┘
                      ▼
                  Database

        AppCore.Model is shared by all layers
```

<!-- TODO: verify the dependency arrows above against the actual .csproj references and adjust. -->

**Guiding ideas**

- **Single core, multiple front ends**: no business logic inside Api / Web / Win.
- **Separation of concerns**: DAL knows about persistence, BLL knows about rules, endpoints know about transport/UI.
- **Auth as its own module**: identity concerns are isolated and can be reused or replaced independently.

---

## Projects

| Project | Responsibility |
|---|---|
| `AppCore.Model` | Shared entities, DTOs, enums and other plain models used across layers. |
| `AppCore.DAL` | Data access: database context, repositories/queries, migrations. |
| `AppCore.BLL` | Business logic: services, validation, application rules. |
| `AppCore.Auth` | Authentication and authorization (users, roles, tokens/claims). |
| `AppCore.Api` | HTTP API endpoint exposing BLL functionality to external clients. |
| `AppCore.Web` | Web front end. |
| `AppCore.Win` | Windows desktop front end. |

<!-- TODO: replace the generic descriptions with specifics once confirmed (ORM, Web framework: MVC/Razor/Blazor, Win framework: WinForms/WPF, auth mechanism: JWT/Identity/cookie). -->

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) 9.0.200 or later (required for `.slnx`)
- Visual Studio 2022 17.13+ (or another IDE with `.slnx` support)
- Windows (required for `AppCore.Win`)
- A supported database server <!-- TODO: e.g. SQL Server -->

### Clone

```bash
git clone https://github.com/sasy07/AppCore.git
cd AppCore
```

### Build

```bash
dotnet restore AppCore.slnx
dotnet build AppCore.slnx -c Release
```

### Run

```bash
# Web API
dotnet run --project AppCore.Api

# Web app
dotnet run --project AppCore.Web

# Windows desktop app
dotnet run --project AppCore.Win
```

---

## Configuration

<!-- TODO: fill in the real settings. Example below. -->

Connection strings and other settings live in each endpoint's `appsettings.json` (do **not** commit real credentials; use User Secrets or environment variables):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AppCore;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

If the DAL uses migrations, apply them before the first run:

```bash
# TODO: adjust to your ORM / project setup
dotnet ef database update --project AppCore.DAL --startup-project AppCore.Api
```

---

## Conventions

- Keep endpoints thin: controllers/forms call BLL services and nothing more.
- Only DAL touches the database.
- Shared contracts (DTOs, enums) live in `AppCore.Model`.
- New features: add the model → DAL → BLL → expose through the desired endpoint(s).

---

## Roadmap

- [ ] Unit tests for BLL
- [ ] Integration tests for DAL / API
- [ ] CI pipeline (build + test) with GitHub Actions
- [ ] Docker support for the API
- [ ] API documentation (Swagger / OpenAPI)

---

## Contributing

Issues and pull requests are welcome. For larger changes, please open an issue first to discuss what you would like to change.

---

## License

<!-- TODO: choose a license (MIT is a common default) and add a LICENSE file. -->
No license specified yet.

---

**Author:** [@sasy07](https://github.com/sasy07)
