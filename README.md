### 🛰️ NutriSpace API — Sistema de Monitoramento de Estufas Espaciais
Projeto Acadêmico desenvolvido para a FIAP > Arquitetura: Web API RESTful com .NET 10 & Entity Framework Core

Banco de Dados: Oracle Database (via ODP.NET Managed Driver)

### 📖 1. O que é o Projeto e para que serve?
O NutriSpace é um ecossistema tecnológico projetado para viabilizar e automatizar a agricultura de precisão em colônias espaciais ou estações orbitais de longa permanência. Em ambientes extraterrestres, o controle climático rigoroso é vital: pequenas oscilações de temperatura ou umidade podem colapsar um microbioma vegetal inteiro, comprometendo o suprimento de oxigênio e alimentos dos tripulantes.

A NutriSpace API atua como o cérebro central (Backend) desse ecossistema. Ela cumpre os seguintes papéis fundamentais:

Centralização de Telemetria IoT: Recebe e processa em tempo real dados de temperatura e umidade enviados por microcontroladores (como o ESP32) instalados nas estufas.

Orquestração de Regras Biológicas: Vincula cada estufa automatizada a uma espécie de planta específica, sabendo de antemão quais são os seus limiares térmicos e hídricos ideais.

Auditoria e Governança Espacial: Permite que astronautas operadores controlem os atuadores (como bombas de irrigação) e extraiam relatórios completos com o histórico de comportamento climático de cada bioma.

### 🛠️ 2. O que instalar na Máquina (Pré-requisitos)
Para compilar, rodar e debugar este projeto localmente, certifique-se de instalar as seguintes ferramentas:

SDK do .NET 10 (ou superior)

Responsável por compilar o código C# e executar a aplicação Kestrel.

Download do .NET

Visual Studio 2022 (Versão 17.12 ou superior) ou VS Code

Caso use o Visual Studio, selecione a carga de trabalho: "Desenvolvimento web e de ASP.NET".

Navegador Web Moderno (Chrome, Edge ou Firefox) para visualização e testes na interface gráfica do Swagger UI.

### 📁 3. Estrutura do Projeto (Pastas e Arquivos Explicados)
A arquitetura do projeto segue o padrão oficial do ASP.NET Core Web API, segmentando responsabilidades em pastas bem definidas:

NutriSpaceAPI/

 📁 Controllers/         # Camada de Exposição (Endpoints REST/HTTP)

 📁 Data/                # Camada de Persistência e Contexto do EF Core

 📁 DTOs/                # Objetos de Transferência de Dados (Data Transfer Objects)
 
 📁 Migrations/          # Histórico de Controle de Versão do Banco de Dados

 📁 Models/              # Entidades do Domínio (Mapeamento de Tabelas)

 📄 appsettings.json     # Arquivo de Configuração (Strings de Conexão e Logs)
 
 📄 NutriSpaceAPI.csproj # Arquivo de Definição do Projeto e Pacotes NuGet
 
 📄 Program.cs           # Inicializador (Bootstrap) da API e Injeção de Dependências

Detalhamento por Pasta:
📁 Controllers/: Contém as classes que herdam de ControllerBase. Elas recebem as requisições HTTP externas da internet, validam as regras básicas de entrada e conversam com o banco de dados para devolver respostas JSON.

📁 Data/: Abriga o AppDbContext.cs. É aqui que se configura o motor do Entity Framework Core, as regras da Fluent API (como as particularidades do Oracle e formatação de precisão de casas decimais) e o registro das tabelas gerenciadas.

📁 DTOs/: Classes utilitárias puras. Servem para filtrar quais campos o usuário precisa enviar nas requisições de criação (CreateDto), evitando expor chaves primárias ou propriedades internas desnecessárias da API.

📁 Migrations/: Pasta gerada pelo comando dotnet ef. Guarda os scripts estruturados em C# que traduzem as entidades orientadas a objetos do C# para a linguagem estruturada do banco SQL (CREATE TABLE, chaves estrangeiras, etc.).

📁 Models/: Contém o coração das regras de negócio do projeto. São as entidades de domínio (Astronauta, Estufa, Planta, LeituraSensor) com suas propriedades e anotações de dados ([Table], [Key], [ForeignKey]).

### 🔌 4. Catálogo Completo de Endpoints (Documentação das Rotas REST)
A API adota o padrão de rotas RESTful baseado em recursos e utiliza códigos de status HTTP semânticos (200 OK, 201 Created, 404 Not Found, 500 Internal Server Error).

### 🛰️ Recurso: Astronauta (/api/Astronauta)
Gerencia o cadastro de operadores humanos responsáveis pelo monitoramento e intervenções nas estufas.

GET /api/Astronauta (Obter Todos)

O que faz: Retorna a lista completa de todos os astronautas registrados na base.

Para que serve: Preencher caixas de seleção (dropdowns) em interfaces móveis ou web.

GET /api/Astronauta/{id} (Obter por ID)

O que faz: Busca o registro detalhado de um astronauta individual.

POST /api/Astronauta (Cadastrar)

O que faz: Insere um novo astronauta informando nome, cargo, e-mail e senha.

Para que serve: Fluxo de registro de novos tripulantes no sistema.

PUT /api/Astronauta/{id} (Atualizar)

O que faz: Substitui integralmente os dados de um astronauta existente com base em seu ID.

Para que serve: Telas de edição de perfil e alteração de cargos ou senhas.

DELETE /api/Astronauta/{id} (Remover)

O que faz: Exclui permanentemente o registro de um astronauta do banco (desde que ele não esteja atrelado ativamente a nenhuma estufa operacional).

### 🌿 Recurso: Planta (/api/Planta)
Dita as assinaturas biológicas e limites climáticos de sobrevivência da flora espacial.

GET /api/Planta (Obter Todas)

O que faz: Retorna todas as espécies cadastradas no sistema.

GET /api/Planta/{id} (Obter por ID)

O que faz: Retorna os detalhes de tolerância climática de uma espécie única.

POST /api/Planta (Cadastrar)

O que faz: Cadastra uma nova espécie definindo seu nome biológico, temperatura mínima e máxima suportada, além da umidade mínima recomendada.

PUT /api/Planta/{id} (Atualizar)

O que faz: Modifica os limiares operacionais biológicos de uma espécie já cadastrada.

DELETE /api/Planta/{id} (Remover)

O que faz: Remove a espécie da base de conhecimento da inteligência da API.

### 🛡️ Recurso: Estufa (/api/Estufa)
O recurso central. Cruza os dados do Astronauta responsável e da Planta cultivada. Possui relacionamento 1:N com os dados de sensores.

GET /api/Estufa (Obter Todas)

O que faz: Lista todas as estufas robóticas da estação.

GET /api/Estufa/{id} (Obter por ID Avançado)

O que faz: Retorna as informações estruturais da estufa e, utilizando carregamento adiantado (Eager Loading via .Include()), traz anexado em um array todo o histórico temporal de leituras registradas pelos sensores.

Para que serve: Alimentar painéis de gráficos em tempo real e dashboards analíticos.

POST /api/Estufa (Criar Estufa)

O que faz: Cria uma nova estufa vinculando-a obrigatoriamente a um idAstronauta existente e a um idPlanta existente.

PUT /api/Estufa/{id} (Atualizar Parâmetros)

O que faz: Altera o nome da estufa ou comuta remotamente o estado operacional dos atuadores, como o campo statusBomba ("Ligado"/"Desligado").

DELETE /api/Estufa/{id} (Desativar)

O que faz: Apaga o registro físico da estufa do mapa de monitoramento.

### 🌡️ Recurso: Leitura do Sensor (/api/LeituraSensor)
Responsável pela ingestão massiva de dados vinda do hardware IoT da estação espacial.

GET /api/LeituraSensor/{id} (Obter Registro Temporário)

O que faz: Recupera uma telemetria específica gerada no passado.

POST /api/LeituraSensor (Inserir Telemetria IoT)

O que faz: Valida de forma segura a existência do alvo (IdEstufa) utilizando checagem de nulidade compatível com bancos Oracle. Se a estufa existir, insere os dados brutos de temperatura e umidade lidos pelo dispositivo. A data e hora exatas (DtHrLeitura) são preenchidas de forma autônoma pelo servidor de banco de dados via gatilho de valor padrão SQL (SYSDATE).

### 🚀 5. Como Executar o Projeto (Guia de Execução Simplificada)
Como o projeto já se encontra configurado de forma nativa com as credenciais de acesso oficiais e as tabelas já foram integralmente propagadas na nuvem Oracle da FIAP, a inicialização local exige apenas 2 passos simples:

Passo 1: Restaurar Pacotes e Dependências
Abra o terminal integrado do Visual Studio ou o PowerShell na pasta raiz do projeto e execute o comando abaixo para baixar e organizar as referências do ecossistema:

dotnet restore

Passo 2: Inicializar o Servidor Kestrel
Execute o comando de inicialização contínua para colocar o servidor de backend ativo:

dotnet run

O terminal entrará em estado de escuta ativa (Event Loop) exibindo os logs de sucesso:
Hosting environment: Development

Now listening on: http://localhost:5092

Passo 3: Explorar a Documentação no Navegador
Mantenha a janela do terminal aberta. Abra qualquer navegador de internet e acesse a interface interativa do Swagger pelo endereço:
👉 http://localhost:5092/swagger

---

## 👥 7. Membros do Grupo

O projeto **NutriSpace** foi desenvolvido pelos seguintes integrantes:

| Nome Completo | RM |
| :--- | :---: |
| Geovanne Coneglian Passos | **RM562573** |
| Lucas Silva Gastão Pinheiro | **R563960** |
| Guilherme Soares de Almeida| **RM563143** |

---
