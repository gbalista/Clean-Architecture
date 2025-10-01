# 🧱 Clean Architecture API Starter (.NET 9 + EF Core + Multitenancy)

Este repositório apresenta uma arquitetura moderna baseada nos princípios de **Clean Architecture**, totalmente modular e extensível, preparada para aplicações empresariais complexas.

Desenvolvido com:
- ✅ .NET 9 Preview + ASP.NET Core
- ✅ Entity Framework Core 9
- ✅ Suporte a múltiplos bancos: PostgreSQL e SQL Server
- ✅ Autenticação e Autorização com Identity + JWT + Role Claims
- ✅ Multi-Tenant (com banco compartilhado)
- ✅ Modularização completa por contexto de negócio (ex: Clinical, Identity, etc)
- ✅ Boas práticas: CORS, Swagger, Validation, Logging, Background Jobs

---

## 🔧 Funcionalidades Principais

- Autenticação com Identity e JWT
- Autorização baseada em Claims e Roles (RoleClaims / UserClaims)
- Suporte a Multi-Tenant com banco compartilhado
- CORS configurado globalmente
- Swagger com versionamento de API
- Jobs assíncronos com Hangfire
- Validações com FluentValidation + validação automática
- Modularização por contexto de negócio
- Suporte a migrações separadas por banco (PostgreSQL e SQL Server)
- Controle de acesso por escopos/permissões (Claims)
- Persistência com EF Core 9 e Fluent Mapping
- Background Services e Scheduled Jobs prontos para uso

---

## 🧱 Estrutura de Projeto

A estrutura segue uma divisão em **módulos independentes**, cada um com suas camadas internas:

```text
Modules/
├── Clinical/
│   ├── Application/      # Casos de uso (Commands, Queries, Handlers)
│   ├── Domain/           # Entidades, Interfaces, Eventos de domínio
│   ├── Infrastructure/   # Persistência, serviços externos
│   └── Endpoints/        # Controllers e rotas da API
├── Identity/
├── Catalog/
└── ... outros módulos ...
```

Esses módulos seguem o padrão **vertical slice** e podem ser usados, substituídos ou removidos independentemente.

---

## 🔄 Adicionando Novos Módulos

Este projeto serve como base — você pode adicionar novos módulos (ex: **Financeiro**, **RH**, **CRM**) seguindo o mesmo padrão:

- Cada módulo possui seu próprio conjunto **Domain / Application / Infrastructure / Endpoints**
- Compartilhamento de contratos e serviços comuns via **Core** ou **Shared**
- Migrations independentes para cada módulo e banco
- Módulos são **desacoplados** e podem evoluir separadamente

---

## 🏗️ Multi-Tenant

- Suporte nativo a multi-tenant com banco compartilhado
- Cada tenant pode ter configurações, conexões e permissões separadas
- Armazenamento de tenants configurável (banco ou seed)
- Inclui entidades como `FshTenantInfo` e `TenantDbContext`

---

## 💾 Banco de Dados

**Exemplo de configuração em `appsettings.json`:**

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

## 🚀 Como Começar

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Gerar migrações (Postgre ou SQL Server)
dotnet ef migrations add Initial_Clinical_PostgreSQL -c ClinicalDbContext -o Migrations/PostgreSQL/Clinical

# 3. Rodar a aplicação
dotnet run --project src/Presentation/WebApi
```

---

## 🧠 Boas Práticas Aplicadas

- Clean Architecture e Injeção de Dependência (DI)
- Modularidade por domínio de negócio
- Separação clara entre camadas
- Respeito ao princípio SOLID
- Logging estruturado
- Startup desacoplada e configurável
- Modularização via `Assembly` + `IServiceCollection` + `MapEndpoints()`

---

## 📁 Estrutura Geral

```text
src/
├── Core/                    # Contratos, tipos comuns e abstrações
├── Shared/                  # Utilitários, DTOs e constantes globais
├── Infrastructure/          # Serviços cross-cutting (cache, e-mail, sms...)
├── Presentation/WebApi/     # Startup da aplicação + Middlewares
├── Migrations/              # Migrations separadas por banco e contexto
│   ├── MSSQL/
│   └── PostgreSQL/
└── Modules/                 # Módulos desacoplados (Clinical, Identity...)
```

---

## 📚 Contribuindo

Sinta-se à vontade para abrir issues, PRs ou forks. Este projeto pode servir como **base para novos sistemas empresariais**, plataformas SaaS e ERPs modulares.

---

## 🧑‍💻 Autor

Desenvolvido por [Gustavo Balista](https://www.linkedin.com/in/gustavobalista/)

---

**⭐ Se este repositório te ajudou, não esqueça de deixar uma estrela!**
