const { Inventory, Material } = require('../models');

module.exports = {
  // GET /inventory
  async index(req, res) {
    try {
      const inventory = await Inventory.findAll({
        include: {
          model: Material,
          as: 'material',
          attributes: ['name', 'category']
        }
      });
      res.json(inventory);
    } catch (error) {
      console.error(error);
      res.status(500).json({ error: 'Erro ao buscar inventário' });
    }
  },

  // POST /inventory
  async store(req, res) {
    try {
      const { material_id, location, quantity } = req.body;
      const newItem = await Inventory.create({ material_id, location, quantity });
      res.status(201).json(newItem);
    } catch (error) {
      console.error(error);
      res.status(500).json({ error: 'Erro ao adicionar item ao estoque' });
    }
  },

  // PUT /inventory/:id
  async update(req, res) {
    try {
      const { location, quantity } = req.body;
      const item = await Inventory.findByPk(req.params.id);

      if (!item) return res.status(404).json({ error: 'Item não encontrado' });

      await item.update({ location, quantity });
      res.json(item);
    } catch (error) {
      console.error(error);
      res.status(500).json({ error: 'Erro ao atualizar item do estoque' });
    }
  },

  // DELETE /inventory/:id
  async destroy(req, res) {
    try {
      const item = await Inventory.findByPk(req.params.id);

      if (!item) return res.status(404).json({ error: 'Item não encontrado' });

      await item.destroy();
      res.json({ message: 'Item removido com sucesso' });
    } catch (error) {
      console.error(error);
      res.status(500).json({ error: 'Erro ao remover item do estoque' });
    }
  }
};
