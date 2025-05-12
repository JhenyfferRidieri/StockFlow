'use strict';
const { Model } = require('sequelize');

module.exports = (sequelize, DataTypes) => {
  class Sale extends Model {
    static associate(models) {
    }
  }

  Sale.init({
    customer_name: {
      type: DataTypes.STRING,
      allowNull: false
    },
    total: {
      type: DataTypes.DECIMAL(10, 2),
      allowNull: false
    },
    status: {
      type: DataTypes.ENUM('pendente', 'pago', 'cancelado'),
      defaultValue: 'pendente'
    }
  }, {
    sequelize,
    modelName: 'Sale',
    tableName: 'sales',
    underscored: true,
    timestamps: true
  });

  return Sale;
};
