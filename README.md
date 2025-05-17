# Dener Viana - implementa o padrão RPC (Remote Procedure Call)

## Visão Geral

Este repositório implementa um sistema de APIs que utilizam o padrão RPC (Remote Procedure Call) sobre RabbitMQ para promover comunicação eficiente entre serviços, com foco em escalabilidade, modularidade e flexibilidade. O projeto é composto por múltiplas aplicações, incluindo APIs para gerenciamento de usuários e clientes, além de um conjunto de "building blocks" reutilizáveis.

## Funcionalidades

- **APIs RESTful para Usuários e Clientes**: Permite listar, cadastrar e consultar entidades de usuários e clientes.
- **Comunicação via RabbitMQ (RPC)**: As APIs utilizam o padrão RPC para comunicação assíncrona e desacoplada entre os serviços.
- **Validação e Segurança**: Validação avançada de rotas, dados e cabeçalhos, incluindo autenticação de requisições.
- **Documentação Automática com Swagger**: Endpoints documentados e exploráveis via Swagger UI.
- **Monitoramento e Health Checks**: Health checks configuráveis e integração com Serilog para monitoramento e logging avançado.
- **Integração com MongoDB e SQL Server**: Suporte a múltiplos backends de dados.
- **Arquitetura Modular**: Uso de "building blocks" para facilitar manutenção e evolução.

## Arquitetura

- **Microserviços/Modular**: Cada aplicação (Users API, Clients API) é separada e utiliza seus próprios contextos de dados, serviços de domínio e repositórios.
- **Camadas**:
  - **Presentation**: Endpoints, validações e configuração de rotas.
  - **Application**: Serviços de aplicação e mapeamentos (AutoMapper).
  - **Domain**: Entidades, interfaces e regras de negócio.
  - **Infra**: Implementação de repositórios, contexto de banco de dados, integrações externas.
  - **Building Blocks**: Componentes compartilhados como middlewares, validações, notificações e interfaces.
- **Padrão de Injeção de Dependências**: Configurado via BootStrapper em cada aplicação.
- **Documentação e Configuração via appsettings.json e SwaggerSettings**.

## Tecnologias Utilizadas

- **.NET 8.0** (C#)
- **RabbitMQ** (mensageria e RPC)
- **MongoDB** (armazenamento de clientes)
- **SQL Server** (armazenamento de usuários)
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **Serilog** (logging)
- **Swagger (Swashbuckle)** (documentação de APIs)
- **Polly** (resiliência)
- **Docker** (opcional para deploy)
- **HealthChecks**

## Configuração do Ambiente

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- RabbitMQ
- MongoDB
- SQL Server

### Variáveis e Configuração

Cada aplicação possui arquivos `appsettings.Development.json` para configurar:

- **MongoDbSettings** (Clients API):
  - `MongoDbConnection`: string de conexão MongoDB
  - `DatabaseName`: nome do banco
- **ConnectionStrings** (Users API):
  - `SqlServerConnection`: string de conexão SQL Server
- **AppSettings**:
  - Chaves de autenticação e URLs permitidas
- **SwaggerSettings**:
  - Título, descrição, contato, licença e versão da API
- **Serilog**:
  - Nível de logs e integração com Elasticsearch

Edite os arquivos de configuração conforme seu ambiente.

### Inicialização

```bash
# Restaurar dependências e compilar
dotnet restore
dotnet build

# Executar APIs (exemplo Users API)
cd src/Users/DenerViana.Rpc.RabbitMQ.Users.Api
dotnet run
```

## Endpoints das APIs

### Exemplos de Endpoints

#### Users API

- `GET /User`: Lista todos os usuários
- `POST /User`: Cria um novo usuário (requer cabeçalhos customizados como `x-origin`, `x-user-id`, `x-user-name`, `x-correlation-id`)
- [Swagger UI disponível em `/swagger`](http://localhost:{porta}/swagger)

#### Clients API

- `GET /Client`: Lista todos os clientes
- `POST /Client`: Cria um novo cliente (requer cabeçalhos customizados)
- [Swagger UI disponível em `/swagger`](http://localhost:{porta}/swagger)

> **Nota:** Para exemplos detalhados de requisições HTTP, consulte os arquivos `.http` presentes em cada projeto (`DenerViana.Rpc.RabbitMQ.Users.Api.http`, `DenerViana.Rpc.RabbitMQ.Clients.Api.http`).

## Testes

- O projeto é estruturado para suportar testes unitários e de integração (verifique se há projetos de teste dedicados).
- Recomenda-se o uso do xUnit, NUnit ou MSTest junto ao FluentAssertions.
- Para executar testes:
  ```bash
  dotnet test
  ```
- As validações de rotas e dados são amplamente cobertas por regras no FluentValidation.

## Licença

Este projeto está licenciado sob a licença **GNU General Public License v3.0 (GPL-3.0)**. Veja o arquivo [LICENSE.md](./LICENSE.md) para mais detalhes.

## Informações Gerais e Contato

- Autor: **Dener Viana**
- Contato: [viana.dener@gmail.com](mailto:viana.dener@gmail.com)
- GitHub: [viana-dener/DenerViana.Rpc.RabbitMQ.Repo](https://github.com/viana-dener/DenerViana.Rpc.RabbitMQ.Repo)
- Para dúvidas técnicas, utilize as Issues do GitHub.

---

> **Para usuários leigos:**  
> Esta aplicação permite criar e consultar dados de usuários e clientes através de APIs modernas, utilizando tecnologia de ponta e segurança.  
> Para começar, basta seguir os passos de configuração e acessar a documentação via navegador.

> **Para usuários técnicos:**  
> O sistema é modular, aderente a boas práticas de engenharia de software, fácil de estender e integrar com outros sistemas via RabbitMQ.

---

**Observação:** Esta documentação foi gerada automaticamente com base na análise do código-fonte em maio de 2025. Para informações completas e atualizadas sobre endpoints, recomenda-se consultar o Swagger da aplicação em execução.

---

> **Limitação de busca:** Os resultados apresentados são baseados em uma busca limitada de arquivos do repositório. Veja mais detalhes e arquivos diretamente no [GitHub Code Search](https://github.com/viana-dener/DenerViana.Rpc.RabbitMQ.Repo/search?type=code).