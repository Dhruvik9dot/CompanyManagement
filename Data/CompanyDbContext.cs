using CompanyManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;

namespace CompanyManagement.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }

        public DbSet<CompanyInfo> CompanyMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }  
}
