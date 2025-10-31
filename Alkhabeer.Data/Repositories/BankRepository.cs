using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alkhabeer.Data.Repositories
{
    public class BankRepository : BaseRepository<Bank>
    {
        public BankRepository(DBContext context) : base(context)
        { }
    }
}
