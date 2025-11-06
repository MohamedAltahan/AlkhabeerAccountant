//using Alkhabeer.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Alkhabeer.Data.Repositories
//{
//    public class PermissionRepository : BaseRepository<Permission>
//    {
//        public PermissionRepository(DBContext context) : base(context) { }

//        public async Task<List<Permission>> GetByIdsAsync(List<int> ids)
//        {
//            return await Table
//                .Where(p => ids.Contains(p.Id))
//                .ToListAsync();
//        }
//    }
//}
