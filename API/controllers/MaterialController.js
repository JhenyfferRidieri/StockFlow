const db = require('../models/db');

exports.getAllMaterials = (req, res) => {
  db.query('SELECT * FROM materials', (err, results) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.status(200).json(results);
  });
};

exports.getMaterialById = (req, res) => {
  const { id } = req.params;
  db.query('SELECT * FROM materials WHERE id = ?', [id], (err, results) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    if (results.length === 0) {
      return res.status(404).json({ message: 'Material não encontrado' });
    }
    res.status(200).json(results[0]);
  });
};

exports.createMaterial = (req, res) => {
  const { name, description, quantity } = req.body;
  if (!name || !quantity) {
    return res.status(400).json({ message: 'Name e quantity são obrigatórios' });
  }

  const sql = 'INSERT INTO materials (name, description, quantity) VALUES (?, ?, ?)';
  db.query(sql, [name, description, quantity], (err, result) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.status(201).json({ message: 'Material criado com sucesso', id: result.insertId });
  });
};

exports.updateMaterial = (req, res) => {
  const { id } = req.params;
  const { name, description, quantity } = req.body;

  const sql = 'UPDATE materials SET name = ?, description = ?, quantity = ? WHERE id = ?';
  db.query(sql, [name, description, quantity, id], (err) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.status(200).json({ message: 'Material atualizado com sucesso' });
  });
};

exports.deleteMaterial = (req, res) => {
  const { id } = req.params;

  const sql = 'DELETE FROM materials WHERE id = ?';
  db.query(sql, [id], (err) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.status(200).json({ message: 'Material deletado com sucesso' });
  });
};