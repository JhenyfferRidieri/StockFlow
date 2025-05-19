const { SaleItem, Material } = require('../models');

module.exports = {
  async index(req, res) {
    const items = await SaleItem.findAll({ include: ['material'] });
    res.json(items);
  },

  async store(req, res) {
    const { sale_id, material_id, quantity, unit_price } = req.body;
    const item = await SaleItem.create({ sale_id, material_id, quantity, unit_price });
    res.status(201).json(item);
  },

  async destroy(req, res) {
    const item = await SaleItem.findByPk(req.params.id);
    if (!item) return res.status(404).json({ error: 'Item não encontrado' });

    await item.destroy();
    res.json({ message: 'Item removido com sucesso' });
  }
};
