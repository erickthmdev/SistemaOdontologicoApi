🦷 SistemaOdontologicoApi

API REST para gestão de clínicas odontológicas, desenvolvida com ASP.NET Core 9. Permite gerenciar pacientes, agendamentos, prontuário odontológico (odontograma) e controle financeiro, com autenticação segura via JWT.

Este projeto foi desenvolvido para demonstrar conhecimento em arquitetura de API REST, modelagem de banco de dados, autenticação e boas práticas de desenvolvimento back-end.


🚀 Tecnologias


ASP.NET Core 9
Entity Framework Core
PostgreSQL
JWT Authentication
Repository Pattern
Dependency Injection
AutoMapper



🏗️ Arquitetura

O projeto segue uma arquitetura em camadas, separando responsabilidades:

SistemaOdontologicoApi/
├── Controllers/     # Endpoints da API
├── Services/        # Regras de negócio
├── Repositories/     # Acesso a dados (EF Core)
├── DTOs/             # Objetos de transferência de dados
├── Models/           # Entidades do banco de dados
└── Mappings/          # Perfis do AutoMapper


Controllers recebem as requisições e delegam a lógica para os Services
Services contêm as regras de negócio e chamam os Repositories
Repositories são responsáveis pelo acesso ao banco de dados via Entity Framework Core
DTOs e AutoMapper evitam expor as entidades do banco diretamente na API



📌 Funcionalidades

👤 Pacientes


Cadastro e gerenciamento de pacientes
Histórico do paciente


📅 Agendamentos


Marcação de consultas
Gerenciamento de status do agendamento


🦷 Prontuário Odontológico


Odontograma interativo
Registro de tratamentos por dente


💰 Financeiro


Controle de custos de tratamentos
Organização de pagamentos


🔐 Autenticação e Segurança


Autenticação via JWT
Rotas protegidas
Controle de acesso por usuário




⚙️ Como Rodar o Projeto

Requisitos


.NET 9 SDK
PostgreSQL


Passo a passo


Clone o repositório:


bashgit clone https://github.com/erickthmdev/SistemaOdontologicoApi.git
cd SistemaOdontologicoApi


Configure a connection string:

Copie appsettings.example.json para appsettings.json
Edite os dados de conexão com o seu banco PostgreSQL



Restaure as dependências:


bashdotnet restore


Aplique as migrations do banco de dados:


bashdotnet ef database update


Rode a API:


bashdotnet run

A API estará disponível em https://localhost:{porta} — se o Swagger estiver configurado, acesse /swagger para testar os endpoints.


👨‍💻 Desenvolvimento

Toda a API foi projetada e desenvolvida por mim, incluindo:


Arquitetura REST
Regras de negócio
Modelagem do banco de dados
Integração com PostgreSQL
Sistema de autenticação JWT
Estrutura geral da aplicação



🎯 Objetivo do Projeto

Este projeto foi criado para aprimorar e demonstrar habilidades em:


Desenvolvimento back-end com ASP.NET Core
Design de API REST
Integração com banco de dados PostgreSQL
Autenticação e autorização
Boas práticas de arquitetura de software



📄 Licença

Este projeto está sob a licença MIT.
