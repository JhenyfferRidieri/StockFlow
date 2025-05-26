using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models.Enum;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
    public class SaleStateService : ISaleStateService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleStateService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<bool> UpdateStatusAsync(int saleId, string newStatus)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);

            if (sale == null)
                return false;

            if (!Enum.TryParse<SaleStatus>(newStatus, true, out var parsedStatus))
                throw new Exception("Status inválido.");

            // Lógica de transição
            if (!CanChangeStatus(sale.Status, parsedStatus))
                throw new Exception($"Não é permitido mudar de {sale.Status} para {parsedStatus}.");

            sale.Status = parsedStatus;
            await _saleRepository.UpdateAsync(sale);
            return true;
        }

        private bool CanChangeStatus(SaleStatus current, SaleStatus next)
        {
            return (current, next) switch
            {
                (SaleStatus.Pendente, SaleStatus.Pago) => true,
                (SaleStatus.Pago, SaleStatus.Enviado) => true,
                (SaleStatus.Enviado, SaleStatus.Entregue) => true,
                (_, SaleStatus.Cancelado) => true, // Pode cancelar de qualquer status
                _ => false
            };
        }
    }
}
