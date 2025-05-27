using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using StockFlowAPI.Models.Enum;

namespace StockFlowAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryMovementRepository _movementRepository;
        private readonly IAccountReceivableRepository _accountReceivableRepository;

        public SaleService(
            ISaleRepository saleRepository,
            IInventoryRepository inventoryRepository,
            IInventoryMovementRepository movementRepository,
            IAccountReceivableRepository accountReceivableRepository)
        {
            _saleRepository = saleRepository;
            _inventoryRepository = inventoryRepository;
            _movementRepository = movementRepository;
            _accountReceivableRepository = accountReceivableRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _saleRepository.GetAllWithItemsAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _saleRepository.GetByIdWithItemsAsync(id);
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            await _saleRepository.AddAsync(sale);
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale)
        {
            await _saleRepository.UpdateAsync(sale);
            return sale;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _saleRepository.GetByIdWithItemsAsync(id);
            if (sale == null)
                return false;

            await _saleRepository.DeleteAsync(sale.Id);
            return true;
        }

        //  Valida��o e baixa de estoque para venda
        public async Task<bool> DecreaseStockForSaleAsync(Sale sale)
        {
            foreach (var item in sale.SaleItems)
            {
                var inventory = await _inventoryRepository.GetByMaterialIdAsync(item.MaterialId);

                if (inventory == null)
                    throw new ArgumentException($"Material ID {item.MaterialId} n�o encontrado no estoque.");

                if (inventory.Quantity < item.Quantity)
                    throw new InvalidOperationException($"Estoque insuficiente para o material {inventory.Material.Name}");

                inventory.Quantity -= item.Quantity;
                await _inventoryRepository.UpdateAsync(inventory);
            }

            return true;
        }

        //  Pagamento da venda
        public async Task<bool> PaySaleAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdWithItemsAsync(saleId);
            if (sale == null) return false;

            if (sale.Status != SaleStatus.Pendente)
                throw new InvalidOperationException("A venda n�o est� no status 'Pendente'.");

            //  Valida e faz a baixa no estoque
            foreach (var item in sale.SaleItems)
            {
                var inventory = await _inventoryRepository.GetByMaterialIdAsync(item.MaterialId);
                if (inventory == null)
                    throw new ArgumentException($"Material ID {item.MaterialId} n�o encontrado no estoque.");

                if (inventory.Quantity < item.Quantity)
                    throw new InvalidOperationException($"Estoque insuficiente para o material {inventory.Material.Name}.");

                inventory.Quantity -= item.Quantity;
                await _inventoryRepository.UpdateAsync(inventory);

                //  Cria hist�rico da movimenta��o
                var movement = new InventoryMovement
                {
                    MaterialId = item.MaterialId,
                    Type = "exit",
                    Quantity = item.Quantity,
                    Description = $"Venda ID {sale.Id}",
                    Date = DateTime.Now
                };
                await _movementRepository.AddAsync(movement);
            }

            //  Cria conta a receber
            var account = new AccountReceivable
            {
                SaleId = sale.Id,
                Description = $"Venda {sale.Id} - {sale.CustomerName}",
                Amount = sale.Total,
                DueDate = DateTime.Now.AddDays(7),
                IsReceived = false
            };
            await _accountReceivableRepository.AddAsync(account);

            //  Atualiza status da venda
            sale.Status = SaleStatus.Pago;
            await _saleRepository.UpdateAsync(sale);

            return true;
        }

        //  Cancelamento da venda
        public async Task<bool> CancelSaleAsync(int saleId, string reason)
        {
            var sale = await _saleRepository.GetByIdWithItemsAsync(saleId);
            if (sale == null) return false;

            if (sale.Status == SaleStatus.Cancelado)
                throw new InvalidOperationException("A venda j� est� cancelada.");

            if (sale.Status == SaleStatus.Entregue)
                throw new InvalidOperationException("N�o � poss�vel cancelar uma venda j� entregue.");

            //  Estorna estoque se estava paga
            if (sale.Status == SaleStatus.Pago)
            {
                foreach (var item in sale.SaleItems)
                {
                    var inventory = await _inventoryRepository.GetByMaterialIdAsync(item.MaterialId);
                    if (inventory != null)
                    {
                        inventory.Quantity += item.Quantity;
                        await _inventoryRepository.UpdateAsync(inventory);

                        //  Cria hist�rico de entrada (devolu��o)
                        var movement = new InventoryMovement
                        {
                            MaterialId = item.MaterialId,
                            Type = "entry",
                            Quantity = item.Quantity,
                            Description = $"Cancelamento Venda ID {sale.Id} - {reason}",
                            Date = DateTime.Now
                        };
                        await _movementRepository.AddAsync(movement);
                    }
                }

                //  Cancela conta a receber
                var account = await _accountReceivableRepository.GetBySaleIdAsync(sale.Id);
                if (account != null)
                {
                    account.IsReceived = false;
                    account.Description += " - CANCELADO";
                    await _accountReceivableRepository.UpdateAsync(account);
                }
            }

            sale.Status = SaleStatus.Cancelado;
            await _saleRepository.UpdateAsync(sale);

            return true;
        }
    }
}
