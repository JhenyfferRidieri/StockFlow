const express = require('express')
const { swaggerUi, swaggerSpec } = require('./swagger')

const app = express()
app.use(express.json())

app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec))

/**
 * @swagger
 * /materials:
 *   get:
 *     summary: Retorna todos os materiais cadastrados
 *     tags:
 *       - Materiais
 *     responses:
 *       200:
 *         description: Sucesso
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items:
 *                 type: object
 *                 properties:
 *                   id:
 *                     type: number
 *                     example: 1
 *                   nome:
 *                     type: string
 *                     example: Ferro
 */
app.get('/materials', (req, res) => {
  res.json([{ id: 1, nome: 'Ferro' }])
})

app.listen(3333, () => console.log('🔥 Server on http://localhost:3333'))
