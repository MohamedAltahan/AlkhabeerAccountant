using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;


namespace Alkhabeer.Service
{
    public class BankService : BaseService<Bank>
    {
        public BankService(BankRepository bankRepository) : base(bankRepository)
        { }

    }
}
