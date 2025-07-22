# Processo de Desenvolvimento

## Como rodar o projeto

1. **Baixar as dependências**

   No diretório raiz do projeto, execute:
   ```bash
   dotnet restore
   ```

2. **Aplicar as migrations e criar o banco de dados**

   No diretório do projeto da API (`ConcessionariaCarvalho/`), execute:
   ```bash
   dotnet ef database update
   ```
   > Se necessário, instale a ferramenta do Entity Framework:
   > ```bash
   > dotnet tool install --global dotnet-ef
   > ```

3. **Rodar a aplicação**

   No diretório do projeto da API (`ConcessionariaCarvalho/`), execute:
   ```bash
   dotnet run
   ```

4. **Rodar via Docker (opcional para o banco de dados)**

   Para subir o SQL Server via Docker:
   ```bash
   docker-compose up -d
   ```

---

## 1. Planejamento
- Definição dos requisitos do sistema.
- Estruturação das camadas do projeto: Domain, Application, Infrastructure e API.

## 2. Modelagem
- Criação das entidades de domínio na pasta `Domain/Entities`.
- Definição de enums e regras de negócio.

## 3. Implementação da Infraestrutura
- Configuração do Entity Framework Core para acesso ao banco de dados.
- Criação do contexto e das migrações.
- Implementação de repositórios e serviços de infraestrutura.

## 4. Implementação da Camada de Aplicação
- Criação dos casos de uso na pasta `Application/UseCase`.
- Implementação de serviços e validações.
- Definição de contratos de comunicação (Requests e Responses).

## 5. Implementação da API
- Criação dos controllers na pasta `ConcessionariaCarvalho/Controllers`.
- Configuração do Swagger para documentação da API.
- Configuração de autenticação e autorização.
---

## Tecnologias Utilizadas

- **.NET (versão 8.0)**: Framework principal para desenvolvimento da aplicação.
- **Entity Framework Core**: ORM para acesso ao banco de dados.
- **Swagger (Swashbuckle.AspNetCore)**: Documentação interativa da API.
- **SQL Server**: Banco de dados relacional utilizado no desenvolvimento (configurado via Docker).
- **FluentValidation**: Validação de dados.

---

## Observações

- O projeto ainda está em desenvolvimento e pode não funcionar completamente em todos os cenários.
- O banco de dados utilizado é o **SQL Server**, configurado via Docker.  
  A senha do usuário `sa` está definida como `Usuario@123` no arquivo `docker-compose.yml` e na string de conexão do `appsettings.Development.json`.

- A string de conexão está localizada em `appsettings.Development.json`.

