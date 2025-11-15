using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Core.Models
{
    internal class Treasury
    {
        public int Id { get; set; }
        public string TreasuryName { get; set; }
        public int AssignedUserId { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }

        public User AssignedUser { get; set; }
    }
}
