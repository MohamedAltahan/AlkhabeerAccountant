using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Data
{
    public class DBContext : DbContext
    {
        // ✅ Required constructor for AddDbContext
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DBContext() { }
        // 🔹 Define your DbSets (tables)
        //“I want to create and manage a table in the database for the entity Setting.”
        //In other words, it’s how EF knows which classes in your code should become database tables.
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Bank> Banks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set your MySQL connection string
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;database=desktop_pos;user=root;password=;port=3307",
                    new MySqlServerVersion(new Version(8, 0, 30))
                );
            }
        }

        // ✅ Apply all model configurations (from Configurations folder)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This scans and applies all IEntityTypeConfiguration<T> automatically
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);

            // ✅ Convert table and column names to snake_case
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Table name
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));

                // Column names
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(ToSnakeCase(property.Name));

                // Keys
                foreach (var key in entity.GetKeys())
                    key.SetName(ToSnakeCase(key.GetName()));

                // Foreign keys
                foreach (var fk in entity.GetForeignKeys())
                    fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName()));

                // Indexes
                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
            }
        }

        // 🔹 Helper method
        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsUpper(c))
                {
                    if (i > 0) sb.Append('_');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
