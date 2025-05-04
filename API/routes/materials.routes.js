const express = require('express');
const router = express.Router();
const MaterialController = require('../controllers/MaterialController');

router.get('/', MaterialController.index);
router.get('/:id', MaterialController.show);
router.post('/', MaterialController.store);
router.put('/:id', MaterialController.update);
router.delete('/:id', MaterialController.destroy);

module.exports = router;
