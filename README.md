# 🧱 Clean Architecture API Starter (.NET 9 + EF Core + DDD + Multitenancy)

Este repositório apresenta uma arquitetura moderna baseada nos princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**, totalmente modular e extensível, preparada para aplicações empresariais reais com suporte a **multitenancy**, **cache distribuído (Redis)**, **múltiplos bancos** e outros recursos avançados.

Desenvolvido com:
- ✅ .NET 9 Preview + ASP.NET Core
- ✅ Entity Framework Core 9
- ✅ Domain-Driven Design (DDD) com Events e Separation of Concerns
- ✅ Multi-Tenant (banco compartilhado)
- ✅ Suporte a PostgreSQL e SQL Server
- ✅ Modularização completa por contexto de negócio (ex: Clinical, Identity, etc)
- ✅ Redis Cache distribuído
- ✅ Rate Limiting via middleware
- ✅ Boas práticas: CORS, Swagger, Logging, Background Jobs, FluentValidation
- ✅ Suporte a eventos de domínio com handlers desacoplados  

---

## 🔧 Funcionalidades Principais

- Autenticação com Identity + JWT
- Controle de acesso por escopos/permissões (Claims)
- Suporte completo a Multi-Tenant com banco compartilhado
- Modularização por vertical slice (contextos de negócio desacoplados)
- Jobs e tarefas assíncronas com Hangfire
- Redis integrado como provedor de cache
- Rate Limiting configurável via appsettings
- Validações com FluentValidation + validação automática
- Logging estruturado com Serilog
- Suporte a migrações separadas por banco e módulo (PostgreSQL e SQL Server)
- Persistência com EF Core 9 e Fluent Mapping
- Background Services e Scheduled Jobs prontos para uso

---

## 🧱 Estrutura de Projeto

O projeto é organizado em **módulos independentes**, cada um seguindo as camadas do DDD:

```text
Modules/
├── Clinical/
│   ├── Domain/           # Entidades, Interfaces, Eventos de Domínio
│   ├── Application/      # UseCases, Commands, Queries, Handlers
│   ├── Infrastructure/   # Persistência (EF Core), serviços externos
│   └── Endpoints/        # Controllers da API (MapEndpoints)
├── Identity/
├── Catalog/
└── ... outros módulos ...
```

## 🔄 Adicionando Novos Módulos

Este projeto serve como base — você pode adicionar novos módulos (ex: **Financeiro**, **RH**, **CRM**) seguindo o mesmo padrão:

- Cada módulo possui seu próprio conjunto **Domain / Application / Infrastructure / Endpoints**
- Compartilhamento de contratos e serviços comuns via **Core** ou **Shared**
- Migrations independentes para cada módulo e banco
- Módulos são **desacoplados** e podem evoluir separadamente

---

## 🧵 DDD e Eventos de Domínio

O projeto implementa DDD na prática, com suporte a **eventos de domínio**:

- Eventos definidos em `Application/<module>/Events/`
- `DomainEventHandlers` reagem a mudanças importantes no domínio (ex: novo paciente cadastrado)
- Uso de `DomainEvent` + `IEventHandler<T>` para manter lógica isolada

---

## 🏗️ Multi-Tenant

- Suporte nativo a multi-tenant com banco compartilhado
- Cada tenant pode ter configurações, conexões e permissões separadas
- Armazenamento de tenants configurável (banco ou seed)
- Inclui entidades como `FshTenantInfo` e `TenantDbContext`

---

## 🗃️ Banco de Dados

Suporte nativo a:

- PostgreSQL
- SQL Server

**Exemplo de configuração (`appsettings.json`):**

```json
"DatabaseOptions": {
  "Provider": "Postgre",
  "ConnectionString": "Host=localhost;Database=MyDb;Username=postgres;Password=..."
}
```
Bancos suportados:

- PostgreSQL
- SQL Server

A troca entre bancos é feita dinamicamente via configuração.

---

## 🛡️ Segurança

- JWT com suporte a múltiplos tenants
- Claims e permissões customizadas via `RoleClaims` e `UserClaims`
- Validação com FluentValidation em todos os endpoints
- CORS com origens permitidas configuráveis por ambiente

---

## 🧪 Testes e Qualidade

- Suporte à validação de entrada automática
- Validação explícita por atributo ou decoradores
- Criação de validators reutilizáveis
- Logging com Serilog e controle por ambiente

---

## 🧵 Jobs e Background Tasks

- Jobs agendados com **Hangfire**
- Tarefas em background executadas de forma paralela
- Setup completo para ambiente de produção (com painel de monitoramento opcional)

---

## 📦 Estrutura da Pasta `Framework`

A pasta `Framework/` centraliza as funcionalidades compartilhadas que servem como base comum para todos os módulos. Ela foi organizada em subprojetos para manter uma separação clara de responsabilidades e facilitar a manutenção:

```
Framework/
├── Core/                # Entidades base, contratos, resultados, serviços e exceções comuns
├── Core.IAM/            # Gerenciamento de autenticação/autorização, claims, permissões e identidades
├── Core.Persistence/    # Abstrações e extensões para integração com EF Core
├── Infrastructure/      # Configuração geral da infraestrutura (CORS, cache, validações, serviços)
```

**Resumo das responsabilidades:**

- `Core`: Tudo que é genérico e pode ser reutilizado por qualquer módulo. Contém interfaces, abstrações, helpers e tipos base.
- `Core.IAM`: Foco em Identity e Authorization. Contém classes de claim, permissões, roles e helpers de autenticação.
- `Core.Persistence`: Reúne extensões e utilitários que facilitam a configuração e uso de EF Core e bancos de dados.
- `Infrastructure`: Configurações compartilhadas como CORS, cache, medição de tempo de requisição, health checks, etc.

Essa estrutura permite que qualquer módulo (como `Clinical`, `Catalog`, `Identity`) aproveite esses recursos sem duplicação de código, promovendo reutilização e manutenção eficiente.

---

## 🚀 Para Rodar

```bash
dotnet restore

# Para PostgreSQL
dotnet ef migrations add Initial_Clinical_PostgreSQL -c ClinicalDbContext -o Migrations/PostgreSQL/Clinical

# Rodar a aplicação
dotnet run --project src/Presentation/WebApi
```

---

## 🧠 Boas Práticas Aplicadas

- ✅ Clean Architecture e separação de responsabilidades
- ✅ DDD com Domain Events e Interfaces
- ✅ Modularização total (cada módulo é isolado)
- ✅ Startup leve e extensível com MapEndpoints()
- ✅ Suporte a migrações modulares
- ✅ Autenticação centralizada + Claims por Roles
- ✅ Logs com Serilog e rate limit configurável
- ✅ HealthChecks, Swagger, CORS
- ✅ Eventos de domínio com handlers isolados  
- ✅ Cache distribuído com Redis integrado

---

## 🧰 Tecnologias e Patterns Usados
- ASP.NET Core 9 com Minimal APIs e DI
- Entity Framework Core 9 com separação por contexto
- Domain-Driven Design (DDD)
- Clean Architecture
- CQRS (Commands / Queries)
- Event-driven Domain Model
- Modular Monolith com vertical slice
- Hangfire para background jobs
- Redis para cache
- Serilog para logging estruturado

---

## 📚 Contribuindo

Esse projeto pode ser usado como base para:

- Novas aplicações empresariais
- Plataformas SaaS multi-tenant
- APIs modulares extensíveis
- Portais de gestão com múltiplos domínios

Pull requests e sugestões são bem-vindas!

---

## 👨‍💻 Autor

Desenvolvido por [Gustavo Balista](https://www.linkedin.com/in/gustavobalista)

---

**⭐ Deixe sua estrela se esse repositório te ajudou!**
