'use strict';
const { Model } = require('sequelize');

module.exports = (sequelize, DataTypes) => {
  class Inventory extends Model {
    static associate(models) {
      Inventory.belongsTo(models.Material, {
        foreignKey: 'material_id',
        as: 'material',
      });
    }
  }

  Inventory.init({
    material_id: {
      type: DataTypes.INTEGER,
      allowNull: false,
    },
    location: {
      type: DataTypes.STRING,
    },
    quantity: {
      type: DataTypes.INTEGER,
      defaultValue: 0,
    }
  }, {
    sequelize,
    modelName: 'Inventory',
    tableName: 'inventory',
    underscored: true,
    timestamps: true,
  });

  return Inventory;
};
