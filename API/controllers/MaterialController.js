const db = require('../models/db');

module.exports = {
  async index(req, res) {
    const [rows] = await db.query('SELECT * FROM materials');
    res.json(rows);
  },

  async show(req, res) {
    const [rows] = await db.query('SELECT * FROM materials WHERE id = ?', [req.params.id]);
    res.json(rows[0]);
  },

  async store(req, res) {
    const { name, quantity, category } = req.body;
    await db.query('INSERT INTO materials (name, quantity, category) VALUES (?, ?, ?)', [name, quantity, category]);
    res.status(201).json({ message: 'Material criado com sucesso' });
  },

  async update(req, res) {
    const { name, quantity, category } = req.body;
    await db.query('UPDATE materials SET name = ?, quantity = ?, category = ? WHERE id = ?', [name, quantity, category, req.params.id]);
    res.json({ message: 'Material atualizado com sucesso' });
  },

  async destroy(req, res) {
    await db.query('DELETE FROM materials WHERE id = ?', [req.params.id]);
    res.json({ message: 'Material excluído com sucesso' });
  }
};
