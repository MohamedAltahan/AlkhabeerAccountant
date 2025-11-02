using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;


namespace Alkhabeer.Service.Banks
{
    public class BankService : BaseService<Bank>
    {
        private readonly BankRepository _bankRepository;

        public BankService(BankRepository bankRepository) : base(bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<Result<Bank>> SaveAsync(Bank bank)
        {
            if (bank.Id == 0)
                return await AddAsync(bank);
            else
                return await UpdateAsync(bank);
        }

        public async Task<PaginatedResult<Bank>> GetPagedAsync(int page, int size)
        {
            return await _bankRepository.GetPagedAsync(page, size);
        }
    }
}
