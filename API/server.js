const express = require('express');
const { swaggerUi, swaggerSpec } = require('./swagger');

const materialRoutes = require('./routes/materials');
const inventoryRoutes = require('./routes/inventory');
const salesRoutes = require('./routes/sales');

const app = express();
app.use(express.json());

// Swagger
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));

app.use('/materials', materialRoutes);
app.use('/inventory', inventoryRoutes);
app.use('/sales', salesRoutes);

app.get('/', (req, res) => {
  res.send('StockFlow API rodando 🚀');
});

const PORT = process.env.PORT || 3333;
app.listen(PORT, () => {
  console.log(`Servidor rodando na porta ${PORT}`);
});
