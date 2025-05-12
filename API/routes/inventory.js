const express = require('express');
const router = express.Router();
const InventoryController = require('../controllers/InventoryController');

router.get('/', InventoryController.index);
router.post('/', InventoryController.store);
router.put('/:id', InventoryController.update);
router.delete('/:id', InventoryController.destroy);

module.exports = router;
