using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Services
{
	public class AccountReceivableService : IAccountReceivableService
	{
		private readonly IAccountReceivableRepository _repository;

		public AccountReceivableService(IAccountReceivableRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<AccountReceivable>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<AccountReceivable?> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<AccountReceivable> CreateAsync(AccountReceivable accountReceivable)
		{
			await _repository.AddAsync(accountReceivable);
			return accountReceivable;
		}

		public async Task<AccountReceivable> UpdateAsync(AccountReceivable accountReceivable)
		{
			await _repository.UpdateAsync(accountReceivable);
			return accountReceivable;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var account = await _repository.GetByIdAsync(id);
			if (account == null) return false;

			await _repository.DeleteAsync(id);
			return true;
		}
	}
}
