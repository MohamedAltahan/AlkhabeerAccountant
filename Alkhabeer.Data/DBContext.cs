using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Alkhabeer.Core;
using Microsoft.EntityFrameworkCore;
namespace Alkhabeer.Data
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set your MySQL connection string
            optionsBuilder.UseMySql(
                "server=localhost;database=desktop_pos;user=root;password=;port=3307",
                new MySqlServerVersion(new Version(8, 0, 30)) // adjust to your MySQL version
            );

        }
            public DbSet<User> Users{ get;set; }
    }
}
