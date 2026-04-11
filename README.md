# 👤 API de Cadastro de Usuários

API REST desenvolvida em C# com .NET para gerenciamento de usuários, com persistência em banco de dados PostgreSQL.

## 🚀 Tecnologias

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

## 📋 Funcionalidades

- ✅ Cadastro de usuários
- ✅ Listagem de todos os usuários
- ✅ Busca de usuário por ID
- ✅ Remoção de usuário
- ✅ Senhas criptografadas com BCrypt
- ✅ Documentação automática com Swagger

## 🗂️ Arquitetura do Projeto
UserApi/
├── Controllers/    → Recebe as requisições HTTP
├── Services/       → Regras de negócio
├── Repositories/   → Acesso ao banco de dados
├── Models/         → Entidades do sistema
├── DTOs/           → Objetos de transferência de dados
└── Data/           → Configuração do Entity Framework

## 🔌 Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/api/user` | Cria um novo usuário |
| `GET` | `/api/user` | Lista todos os usuários |
| `GET` | `/api/user/{id}` | Busca usuário por ID |
| `DELETE` | `/api/user/{id}` | Remove um usuário |

## ⚙️ Como rodar localmente

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Passo a passo

**1 — Clone o repositório**
```bash
git clone https://github.com/SEU_USUARIO/api-cadastro-usuarios.git
cd api-cadastro-usuarios
```

**2 — Configure o banco de dados**

No arquivo `appsettings.json`, ajuste a connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=userapi_db;Username=postgres;Password=SUA_SENHA"
}
```

**3 — Rode as migrations**
```bash
dotnet ef database update
```

**4 — Inicie a aplicação**
```bash
dotnet run
```

**5 — Acesse o Swagger**

http://localhost:XXXX/swagger

## 📦 Exemplo de requisição

**Criar usuário — POST /api/user**
```json
{
  "name": "João Silva",
  "email": "joao@email.com",
  "password": "123456"
}
```

**Resposta — 201 Created**
```json
{
  "id": 1,
  "name": "João Silva",
  "email": "joao@email.com",
  "createdAt": "2024-01-01T00:00:00"
}
```

## 👨‍💻 Autor

Feito por **Honnyedson** — [GitHub](https://github.com/HonnyedsonCruz) · [LinkedIn]([https://linkedin.com/honnyedson-cruz2005/](https://www.linkedin.com/in/honnyedson-cruz2005/))
