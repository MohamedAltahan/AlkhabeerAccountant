using Alkhabeer.Core.Models;
using Alkhabeer.Core.Shared;
using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;

namespace Alkhabeer.Services
{
    public class PaymentMethodService : BaseService<PaymentMethod>
    {
        public PaymentMethodService(PaymentMethodRepository repo)
            : base(repo)
        {
        }
    }
}
