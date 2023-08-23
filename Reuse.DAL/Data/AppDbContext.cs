using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Branch> branches { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<VDC> VDCs { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<BranchType> BranchTypes { get; set; }

        public DbSet<FiscalYear> FiscalYear { get; set; }

        public DbSet<BlackListToken> BlackListTokens { get; set;}

        public DbSet<FinancialStatement> FinancialStatements { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<ExpensesHint> ExpensesHints { get; set;}

        public DbSet<WoodBusiness> WoodBusinesss { get; set;}
        public DbSet<TaxRate> TaxRates { get; set; }
      
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //        new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
        //        new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
        //        );
        //}
    }

   
}
