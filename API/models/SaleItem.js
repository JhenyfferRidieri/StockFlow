'use strict';
const { Model } = require('sequelize');

module.exports = (sequelize, DataTypes) => {
  class SaleItem extends Model {
    static associate(models) {
      SaleItem.belongsTo(models.Sale, {
        foreignKey: 'sale_id',
        as: 'sale'
      });
      SaleItem.belongsTo(models.Material, {
        foreignKey: 'material_id',
        as: 'material'
      });
    }
  }

  SaleItem.init({
    sale_id: DataTypes.INTEGER,
    material_id: DataTypes.INTEGER,
    quantity: DataTypes.INTEGER,
    unit_price: DataTypes.DECIMAL
  }, {
    sequelize,
    modelName: 'SaleItem',
    tableName: 'sale_items',
    underscored: true,
    timestamps: true
  });

  return SaleItem;
};
