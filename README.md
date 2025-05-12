# 🛒 StockFlow

StockFlow é uma aplicação focada em **gestão de materiais, estoque e fluxo de vendas**, desenvolvida como parte de um desafio prático individual da disciplina de Sistemas Corporativos (Análise e Desenvolvimento de Sistemas - 2025/1).

---

## 🚀 Tecnologias Utilizadas

- **Backend:** Node.js + Express
- **ORM:** Sequelize
- **Banco de dados:** MySQL
- **Documentação da API:** Swagger (em `/api-docs`)
- **Versionamento:** Git + GitHub
- **Frontend:** React (em desenvolvimento)

---

## 📌 Requisitos do Projeto

- [x] Desenvolver uma API RESTful com dados em formato JSON
- [x] Versionamento completo com Git e repositório público no GitHub
- [x] Desenvolver em uma Pull Request (sem merge na main até o final)
- [x] Realizar pelo menos 1 commit por semana, de 29/04 a 27/05
- [ ] Apresentar um diagrama BPMN do fluxo do sistema
- [ ] Apresentar o projeto em defesa oral (explicação técnica e lógica do código)

---

## ✅ Módulos do Sistema

| Módulo                    | Status           | Observações                                        |
|--------------------------|------------------|----------------------------------------------------|
| Gestão de materiais      | ✅ Implementado   | CRUD com Sequelize, rotas REST, documentação       |
| Inventário de estoque    | ✅ Implementado   | Relacionado com materiais, controla o estoque      |
| Processo de vendas       | ✅ Implementado   | CRUD com status (máquina de estados simples)       |
| Carrinho de compras      | ❌ Pendente       | A ser implementado com tabela `SaleItem`           |
| Máquinas de estados      | ⚠️ Parcial        | Estados implementados em `sales`, mas sem FSM doc  |
| Contas a pagar           | ❌ Pendente       | A ser desenvolvido com vencimento, valor e status  |
| Contas a receber         | ❌ Pendente       | Relacionado às vendas efetuadas                    |
| Gestão de funcionários   | ❌ Pendente       | Cadastro básico com nome, cargo e salário          |
| Relatórios contábeis     | ❌ Pendente       | Endpoint de resumo financeiro                      |

---

## 📁 Estrutura de Pastas

```bash
StockFlow/
├── API/                     # API REST com Node.js + Express + Sequelize
│   ├── controllers/         # Lógica de negócio
│   ├── migrations/          # Migrations do Sequelize
│   ├── models/              # Models do Sequelize
│   ├── routes/              # Rotas REST
│   ├── config/              # Config do banco de dados
│   ├── swagger.js           # Configuração Swagger
│   └── server.js            # Entrada principal da aplicação
├── frontend/                # (opcional) React em desenvolvimento
└── README.md                # Documentação geral do projeto
