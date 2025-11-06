using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;


namespace Alkhabeer.Service
{
    public class BankService : BaseService<Bank>
    {
        private readonly BankRepository _bankRepository;

        public BankService(BankRepository bankRepository) : base(bankRepository)
        {
            _bankRepository = bankRepository;
        }

    }
}
