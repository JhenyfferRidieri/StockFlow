'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    await queryInterface.createTable('sales', {
      id: {
        type: Sequelize.INTEGER,
        autoIncrement: true,
        primaryKey: true
      },
      customer_name: {
        type: Sequelize.STRING,
        allowNull: false
      },
      total: {
        type: Sequelize.DECIMAL(10, 2),
        allowNull: false
      },
      status: {
        type: Sequelize.ENUM('pendente', 'pago', 'cancelado'),
        defaultValue: 'pendente'
      },
      created_at: {
        allowNull: false,
        type: Sequelize.DATE,
        defaultValue: Sequelize.NOW
      },
      updated_at: {
        allowNull: false,
        type: Sequelize.DATE,
        defaultValue: Sequelize.NOW
      }
    });
  },

  down: async (queryInterface) => {
    await queryInterface.dropTable('sales');
  }
};
