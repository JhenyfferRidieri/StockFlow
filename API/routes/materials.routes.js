const express = require('express');
const router = express.Router();
const MaterialController = require('../controllers/MaterialController');

router.get('/', MaterialController.index);
router.get('/:id', MaterialController.show);
router.post('/', MaterialController.store);
router.put('/:id', MaterialController.update);
router.delete('/:id', MaterialController.destroy);

module.exports = router;


/**
 * @swagger
 * tags:
 *   name: Materiais
 *   description: Endpoints para gestão de materiais
 */

/**
 * @swagger
 * /materials:
 *   get:
 *     summary: Retorna todos os materiais
 *     tags: [Materiais]
 *     responses:
 *       200:
 *         description: Lista de materiais
 */

/**
 * @swagger
 * /materials/{id}:
 *   get:
 *     summary: Retorna um material por ID
 *     tags: [Materiais]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: integer
 *     responses:
 *       200:
 *         description: Sucesso
 */

