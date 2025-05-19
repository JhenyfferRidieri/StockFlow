const express = require('express');
const router = express.Router();
const SaleItemController = require('../controllers/SaleItemController');

router.get('/', SaleItemController.index);
router.post('/', SaleItemController.store);
router.delete('/:id', SaleItemController.destroy);

module.exports = router;
