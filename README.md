# DenerViana.Rpc.RabbitMQ.Repo

---

## 🚀 Visão Geral

Este repositório contém uma aplicação em .NET para comunicação RPC (Remote Procedure Call) utilizando RabbitMQ como broker de mensagens. O projeto tem como objetivo demonstrar a integração entre serviços via mensageria assíncrona, facilitando a troca de informações entre aplicações distribuídas, promovendo escalabilidade e desacoplamento.

---

## ⚙️ Funcionalidades

- **Comunicação RPC via RabbitMQ**
  - Implementação de servidor e cliente RPC.
  - Manipulação de filas, troca de mensagens e callbacks.
- **Configuração Simples**
  - Definição de parâmetros de conexão diretamente via arquivo de configuração.
- **Exemplo de Integração**
  - Exemplo prático de chamada remota entre aplicações usando RabbitMQ como transporte.

---

## 🛠️ Arquitetura e Estrutura

O projeto segue princípios de separação de responsabilidades:

- **/Server**: Responsável por receber, processar requisições e enviar respostas via RabbitMQ.
- **/Client**: Responsável por enviar requisições RPC e aguardar as respostas do servidor.
- **/Common**: Classes utilitárias, contratos de mensagem e configuração.
- **appsettings.json**: Configurações de conexão com o RabbitMQ.

---

## 🧰 Tecnologias Utilizadas

- **.NET**: Framework principal para desenvolvimento da aplicação.
- **RabbitMQ.Client**: Biblioteca para integração com o RabbitMQ.
- **RabbitMQ**: Broker de mensagens para comunicação assíncrona.
- **Serilog** (opcional): Logging estruturado.

---

## 🛠️ Configuração do Ambiente

### Pré-requisitos

- **.NET SDK** instalado.
- **RabbitMQ** em execução (pode ser local ou em container Docker).

### Passos para Configuração

1. Clone o repositório:
   ```bash
   git clone https://github.com/viana-dener/DenerViana.Rpc.RabbitMQ.Repo.git
   ```
2. Navegue até a pasta do projeto:
   ```bash
   cd DenerViana.Rpc.RabbitMQ.Repo
   ```
3. Restaure os pacotes:
   ```bash
   dotnet restore
   ```
4. Configure as credenciais e endereço do RabbitMQ no arquivo `appsettings.json`.
5. Execute a aplicação (ajuste conforme o projeto principal, ex: Client ou Server):
   ```bash
   dotnet run --project ./Server
   # ou
   dotnet run --project ./Client
   ```

---

## 📄 Exemplos de Uso

### Enviando uma requisição RPC

Descreva aqui um exemplo de comando ou chamada feita pelo Client, e a resposta processada pelo Server.

---

## 🧪 Testes

Inclua aqui, se houver, instruções para executar testes automatizados ou manuais.

---

## 📝 Licença

Este projeto está licenciado sob os termos da licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---
