# 🛒 StockFlow

StockFlow é uma aplicação de **gestão de materiais, controle de estoque e processo de vendas**, desenvolvida como parte do projeto da disciplina **Sistemas Corporativos - 2025/1**.

---

## 🚀 Tecnologias Utilizadas

* **Backend:** .NET 8 + ASP.NET Core
* **ORM:** Entity Framework Core + Pomelo (MySQL)
* **Banco de Dados:** MySQL
* **Documentação da API:** Swagger (via Swashbuckle)
* **Versionamento:** Git + GitHub

---

## 📌 Requisitos do Projeto

* ✅ API RESTful servindo e recebendo dados em **JSON**
* ✅ Versionamento completo no Git com repositório público no **GitHub**
* ✅ Desenvolvimento em **1 Pull Request (PR)** (sem merge na `main`)
* ✅ Pelo menos **1 commit por semana** entre **29/04 e 27/05**
* ⚙️ Entregar o projeto com **README atualizado** e **diagrama BPMN**
* 📑 Defesa oral explicando a estrutura e a lógica do código

---

## ✅ Módulos do Sistema

| 🔧 Módulo                   | ✔️ Status      | 🔎 Descrição                                                |
| --------------------------- | -------------- | ----------------------------------------------------------- |
| Gestão de materiais         | ✅ Implementado | CRUD de produtos (nome, cor, tamanho, preço, descrição)     |
| Inventário de estoque       | ✅ Implementado | Controle de estoque atrelado aos materiais                  |
| Processo de vendas          | ✅ Implementado | Vendas com controle de itens (`Sale` e `SaleItem`)          |
| Carrinho de compras         | ❌ Pendente     | A ser implementado                                          |
| Máquina de estados (vendas) | ⚠️ Parcial     | Status na entidade `Sale` (`Pendente`, `Pago`, `Cancelado`) |
| Contas a pagar              | ❌ Pendente     | Cadastro de despesas (fornecedores, contas, serviços)       |
| Contas a receber            | ❌ Pendente     | Recebíveis gerados pelas vendas                             |
| Gestão de funcionários      | ❌ Pendente     | Cadastro básico (nome, cargo, salário)                      |
| Relatórios contábeis        | ❌ Pendente     | Endpoint de resumo financeiro, vendas e fluxo de caixa      |

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
```

---

## ▶️ Como Rodar o Projeto Localmente

### 🛠️ Pré-requisitos

* ✅ [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* ✅ [MySQL](https://www.mysql.com/) rodando localmente

### 📥 Clonar o repositório

```bash
git clone https://github.com/seu-usuario/StockFlow.git
cd StockFlow/StockFlowAPI
```

### ⚙️ Configurar conexão com MySQL

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=StockFlow;user=root;password=sua_senha"
  }
}
```

### 🚀 Aplicar as migrations e gerar o banco:

```bash
dotnet ef database update
```

### ▶️ Rodar o projeto:

```bash
dotnet run
```

A API estará disponível em:

```
http://localhost:5000/swagger
```

---

## 📝 Requisitos Funcionais

* CRUD completo de materiais, estoque, vendas e itens de venda.
* Processamento de vendas com cálculo de total.
* Controle de estoque vinculado à venda.
* Persistência de dados no MySQL.
* Documentação da API via Swagger.

## 🚫 Requisitos Não Funcionais

* API desenvolvida em arquitetura REST.
* Entrega dos dados no formato JSON.
* Documentação técnica no Swagger.
* Projeto versionado com Git no GitHub.
* Código desenvolvido seguindo boas práticas de Clean Code e organização em camadas.

---

## 📈 Diagrama BPMN

👉 **

---

## 👨‍💻 Autor

* **Jhenyffer Oliveira**
  Desenvolvido como parte do curso de **Análise e Desenvolvimento de Sistemas - Universidade Positivo (2025/1)**.

---

## ⭐ Observação

> Este projeto faz parte de uma avaliação acadêmica e foi desenvolvido exclusivamente para fins educacionais.