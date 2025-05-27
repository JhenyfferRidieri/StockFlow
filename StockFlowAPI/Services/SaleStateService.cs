using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using StockFlowAPI.Models.Enum;

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
            var sale = await _saleRepository.GetByIdWithItemsAsync(saleId);
            if (sale == null)
                throw new ArgumentException("Venda não encontrada.");

            var statusAtual = sale.Status;
            var statusNovo = Enum.Parse<SaleStatus>(newStatus, ignoreCase: true);

            // Validação de transições
            if (statusNovo == SaleStatus.Enviado && statusAtual != SaleStatus.Pago)
                throw new InvalidOperationException("Só pode Enviar uma venda que está Paga.");

            if (statusNovo == SaleStatus.Entregue && statusAtual != SaleStatus.Enviado)
                throw new InvalidOperationException("Só pode Entregar uma venda que está Enviada.");

            if (statusNovo == SaleStatus.Cancelado &&
                (statusAtual == SaleStatus.Entregue || statusAtual == SaleStatus.Devolvido))
                throw new InvalidOperationException("Não pode Cancelar uma venda já Entregue ou Devolvida.");

            // Se passou nas regras, atualiza
            sale.Status = statusNovo;
            await _saleRepository.UpdateAsync(sale);

            return true;
        }
    }
}
