const { Sale } = require('../models');

module.exports = {
  async index(req, res) {
    const sales = await Sale.findAll();
    res.json(sales);
  },

  async store(req, res) {
    const { customer_name, total } = req.body;
    const sale = await Sale.create({ customer_name, total });
    res.status(201).json(sale);
  },

  async updateStatus(req, res) {
    const { status } = req.body;
    const sale = await Sale.findByPk(req.params.id);

    if (!sale) return res.status(404).json({ error: 'Venda não encontrada' });

    sale.status = status;
    await sale.save();

    res.json(sale);
  },

  async destroy(req, res) {
    const sale = await Sale.findByPk(req.params.id);
    if (!sale) return res.status(404).json({ error: 'Venda não encontrada' });

    await sale.destroy();
    res.json({ message: 'Venda excluída com sucesso' });
  }
};
