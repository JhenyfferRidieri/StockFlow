# 🛒 StockFlow

StockFlow é uma aplicação de **gestão de materiais, controle de estoque e processo de vendas**, desenvolvida como parte do projeto da disciplina **Sistemas Corporativos - 2025/1**.

---

## 🚀 Tecnologias Utilizadas

- **Backend:** .NET 8 + ASP.NET Core
- **ORM:** Entity Framework Core + Pomelo (MySQL)
- **Banco de Dados:** MySQL
- **Documentação da API:** Swagger (via Swashbuckle)
- **Versionamento:** Git + GitHub
- **Autenticação:** JWT (JSON Web Token)

---

## 🔑 Autenticação

A API utiliza autenticação baseada em **JWT**.

- Gere um token no endpoint:

```

POST /api/Auth/login

```

- Insira no Swagger clicando no botão **"Authorize"**, utilizando o padrão:

```

Bearer {seu\_token}

````

> As rotas estão protegidas de acordo com o perfil de usuário (`Admin`, `Employee` ou `Customer`).

---

## 📌 Requisitos do Projeto

- ✅ API RESTful servindo e recebendo dados em **JSON**
- ✅ Versionamento completo no Git com repositório público no **GitHub**
- ✅ Desenvolvimento em **1 Pull Request (PR)** (sem merge na `main`)
- ✅ Pelo menos **1 commit por semana** entre **29/04 e 27/05**
- ⚙️ Entregar o projeto com **README atualizado** e **diagrama BPMN**
- 📑 Defesa oral explicando a estrutura e a lógica do código

---

## ✅ Módulos do Sistema

| 🔧 Módulo                   | ✔️ Status      | 🔎 Descrição                                                 |
| --------------------------- | -------------- | ------------------------------------------------------------ |
| Gestão de materiais         | ✅ Implementado | CRUD de produtos (nome, cor, tamanho, preço, descrição)     |
| Inventário de estoque       | ✅ Implementado | Controle de estoque atrelado aos materiais                  |
| Processo de vendas          | ✅ Implementado | Vendas com controle de itens (`Sale` e `SaleItem`)          |
| Carrinho de compras         | ✅ Implementado | CRUD de itens no carrinho antes de fechar a venda           |
| Máquina de estados (vendas) | ✅ Implementado | Endpoint para atualizar status da venda                     |
| Contas a pagar              | ✅ Implementado | Cadastro de despesas, status (Pendente, Pago)               |
| Contas a receber            | ✅ Implementado | Gerado automaticamente ao criar uma venda                   |
| Gestão de funcionários      | ✅ Implementado | Cadastro de funcionários (nome, cargo, salário)             |
| Relatórios contábeis        | ✅ Implementado | Endpoint `/api/reports/financial` com resumo financeiro     |

---

## 🗂️ Estrutura do Projeto

```plaintext
StockFlowAPI/
├── Controllers/         # Controladores da API (endpoints)
├── Data/                # Configuração do DbContext (Entity Framework)
├── Interfaces/          # Interfaces de Repositories e Services
│   ├── IRepository/
│   └── IServices/
├── Models/              # Models (Entidades do banco)
├── Repositories/        # Implementações dos Repositories
├── Services/            # Implementações das regras de negócio (Services)
├── Program.cs           # Configuração do app (Swagger, Cors, DI, etc.)
├── StockFlowAPI.csproj  # Arquivo de configuração do projeto
└── README.md            # Documentação do projeto
````

---

## ▶️ Como Rodar o Projeto Localmente

### 🛠️ Pré-requisitos

* ✅ [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* ✅ [MySQL](https://www.mysql.com/) rodando localmente

---

### 📥 Clonar o repositório

```bash
git clone https://github.com/JhenyfferRidieri/StockFlow.git
cd StockFlow/StockFlowAPI
```

---

### ⚙️ Configurar conexão com MySQL

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=StockFlow;user=root;password=SuaSenhaAqui"
  },
  "Jwt": {
    "Key": "suaChaveSuperSecretaAqui",
    "Issuer": "StockFlowAPI",
    "Audience": "StockFlowAPIUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

> 🔑 Recomendo definir uma chave segura no campo `"Jwt:Key"`.

---

### 🚀 Aplicar as migrations e gerar o banco:

```bash
dotnet ef database update
```

---

### ▶️ Rodar o projeto:

```bash
dotnet run
```

* Acesse o Swagger:

```
http://localhost:5000/swagger
```

> ⚠️ Se a porta 5000 não abrir, verifique qual porta foi atribuída no console de execução (`http://localhost:{porta}`).

---

## 🔧 Dados de Teste

| Papel        | Email                                                   | Senha      |
| ------------ | ------------------------------------------------------- | ---------- |
| **Admin**    | [admin@stockflow.com](mailto:admin@stockflow.com)       | Admin123   |
| **Employee** | [employee@stockflow.com](mailto:employee@stockflow.com) | teste\@123 |

* 🔐 Cadastre usuários manualmente via `/api/Auth/register` ou insira diretamente no banco.

---

## 📝 Funcionalidades do Sistema

* 🔹 Gestão de materiais
* 🔹 Controle de inventário
* 🔹 Processo de vendas
* 🔹 Carrinho de compras
* 🔹 Máquina de estados nas vendas (Pendente, Pago, Cancelado, Enviado, Entregue)
* 🔹 Contas a pagar
* 🔹 Contas a receber
* 🔹 Gestão de funcionários
* 🔹 Relatórios contábeis

---

## 🚫 Requisitos Não Funcionais

* API em arquitetura REST
* Dados em JSON
* Documentação no Swagger
* Versionamento completo com Git + GitHub
* Código limpo, organizado, seguindo boas práticas de Clean Code e divisão em camadas

---

## 📈 Diagrama BPMN

![Diagrama BPMN](./StockFlowAPI/docs/bpmn-diagram.jpg)

📥 [Clique aqui para baixar o diagrama BPMN](./StockFlowAPI/docs/bpmn-diagram.jpg)

---

## 👨‍💻 Autora

* **Jhenyffer Oliveira**
  Desenvolvido como parte do curso de **Análise e Desenvolvimento de Sistemas - Universidade Positivo (2025/1)**.

---

## ⭐ Observação

> Este projeto faz parte de uma avaliação acadêmica e foi desenvolvido exclusivamente para fins educacionais.