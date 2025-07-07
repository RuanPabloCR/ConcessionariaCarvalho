# Processo de Desenvolvimento

## 1. Planejamento
- Definição dos requisitos do sistema.
- Estruturação das camadas do projeto: Domain, Application, Infraestructure e API.

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
- Configuração de autenticação e autorização, se necessário.
---

## Tecnologias Utilizadas

- **.NET (versão 8.0)**: Framework principal para desenvolvimento da aplicação.
- **Entity Framework Core**: ORM para acesso ao banco de dados.
- **Swagger (Swashbuckle.AspNetCore)**: Documentação interativa da API.
- **MySQL**: Banco de dados relacional.
- **FluentValidation**: Validação de dados.
---

