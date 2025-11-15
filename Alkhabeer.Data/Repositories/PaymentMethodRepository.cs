using Alkhabeer.Core.Models;

namespace Alkhabeer.Data.Repositories
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>
    {
        public PaymentMethodRepository(DBContext context) : base(context)
        {
        }
    }
}
