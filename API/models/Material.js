'use strict';
const { Model } = require('sequelize');

module.exports = (sequelize, DataTypes) => {
  class Material extends Model {
    static associate(models) {
      Material.hasMany(models.Inventory, {
        foreignKey: 'material_id',
        as: 'inventory'
      });
    }
  }

  Material.init({
    name: {
      type: DataTypes.STRING,
      allowNull: false
    },
    quantity: {
      type: DataTypes.INTEGER,
      defaultValue: 0
    },
    category: {
      type: DataTypes.STRING
    }
  }, {
    sequelize,
    modelName: 'Material',
    tableName: 'materials',
    underscored: true,
    timestamps: true
  });

  return Material;
};
