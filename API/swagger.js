const swaggerJSDoc = require('swagger-jsdoc')
const swaggerUi = require('swagger-ui-express')

const options = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'StockFlow API',
      version: '1.0.0',
      description: 'Documentação da API do StockFlow - Gestão de Materiais e Vendas',
    },
    servers: [
      {
        url: 'http://localhost:3333',
      },
    ],
  },
  apis: ['./server.js'], 
}

const swaggerSpec = swaggerJSDoc(options)

module.exports = {
  swaggerUi,
  swaggerSpec,
}
