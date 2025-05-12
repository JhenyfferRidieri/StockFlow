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


app.listen(3333, () => {
  console.log('🔥 Server on http://localhost:3333');
});

