using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Core.Models
{
    public class Setting
    {
        public long Id { get; set; }

        public string Key { get; set; } = string.Empty;// required
        public string? Value { get; set; }
        public string Type { get; set; } = "string";// default to string
        public string Group { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
