Aqui está um README atualizado considerando os padrões e estrutura que você utilizou no projeto.  

---  

# 🏢 CoworkingBookingSystem  

**CoworkingBookingSystem** é uma API RESTful para gerenciamento de reservas em espaços de coworking, utilizando **DDD (Domain-Driven Design)**, **CQRS (Command and Query Responsibility Segregation)**, **Fail Fast Validation**, e **Pattern Repository**.  

## 📌 Tecnologias Utilizadas  

- **.NET 9**  
- **Entity Framework Core**  
- **MediatR** (para CQRS)  
- **FluentValidation** (para validações Fail Fast)  
- **xUnit e Moq** (para testes unitários)  
- **Swagger** (para documentação da API)  

## 🏛️ Arquitetura  

O projeto está dividido em **quatro camadas principais**, seguindo os princípios do **DDD** e **CQRS**:  

### 1️⃣ `CoworkingBookingSystem.Domain`  
- Contém **entidades**, **comandos**, **queries**, **handlers**, **repositórios** e **regras de negócio**.  
- Define a lógica central do sistema e mantém a separação de responsabilidades.  
- Exemplo de diretórios:  
  - `Entities/` → Contém entidades do domínio como `UserEntity`, `ReservationEntity`, `RoomEntity`.  
  - `Commands/` → Define os comandos usados para modificar o estado do sistema.  
  - `Queries/` → Define as queries para buscar dados.  
  - `Handlers/` → Implementa a lógica dos comandos e queries.  
  - `Repositories/` → Interface para acesso aos dados.  

### 2️⃣ `CoworkingBookingSystem.Domain.API`  
- Contém os **controllers**, expõe os endpoints da API e gerencia a configuração.  
- Diretórios principais:  
  - `Controllers/` → Define os endpoints RESTful.  
  - `appsettings.json` → Configurações da aplicação.  
  - `Program.cs` → Configuração inicial da API.  

### 3️⃣ `CoworkingBookingSystem.Domain.Infra`  
- Implementação dos repositórios e configuração do banco de dados usando **Entity Framework Core**.  
- Diretórios principais:  
  - `Contexts/` → Contexto do banco de dados (DbContext).  
  - `Repositories/` → Implementação dos repositórios do **Pattern Repository**.  

### 4️⃣ `CoworkingBookingSystem.Domain.Tests`  
- Contém os testes unitários e de integração.  
- Estruturado para testar comandos, queries, entidades, handlers e repositórios.  
- Diretórios principais:  
  - `CommandTests/`  
  - `QueryTests/`  
  - `EntityTests/`  
  - `HandlerTests/`  
  - `Repositories/`  

## 🔥 Principais Funcionalidades  

✔ Gerenciamento de usuários e autenticação  
✔ Criação, edição e cancelamento de reservas  
✔ Regras de negócio avançadas (ex: limite de reservas por usuário)  
✔ Padrão **CQRS** para segregação entre leitura e escrita  
✔ Validações **Fail Fast** com **FluentValidation**  

## 📖 Documentação  

## 🧪 Testes  

Para executar os testes automatizados:  
```sh
dotnet test  
```  

## 📌 Melhorias Futuras  

🚀 Implementação de autenticação JWT  
🚀 Suporte para eventos assíncronos com RabbitMQ ou Azure Queue Storage  
