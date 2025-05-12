const express = require('express');
const router = express.Router();
const SaleController = require('../controllers/SaleController');

router.get('/', SaleController.index);
router.post('/', SaleController.store);
router.put('/:id/status', SaleController.updateStatus);
router.delete('/:id', SaleController.destroy);

module.exports = router;
