using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reuse.Bll.DTO;
using Reuse.Bll.Service.Interface;
using Reuse.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Implementation
{
    public class SeedServices : ISeedServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public SeedServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = appDbContext;
            _configuration = configuration;
        }
        public async Task SeedData()
        {
            await SeedAllData();
        }

      

        //public async Task SeedData()
        //{
        //    await SeedAllData();
        //}

        public async Task SeedUser(int BranchId)
        {

            using (var dbContext = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
                    }

                    var checkUser = await _userManager.FindByNameAsync("SuperAdmin");
                    if (checkUser == null)
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            UserName = "SuperAdmin",
                            SecurityStamp = Guid.NewGuid().ToString(),
                            Email = "superadmin@gmail.com",
                            BranchId = BranchId,
                            IsActive = true
                        };

                        string password = "Admin@123";

                        await _userManager.CreateAsync(user, password);
                        if (await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                        {
                            await _userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
                        }

                    }

                    await dbContext.CommitAsync();
                    await dbContext.DisposeAsync();
                }
                catch (Exception)
                {
                    dbContext.Rollback();
                    dbContext.Dispose();
                    throw;
                }
            }
        }

        private async Task SeedAllData()
        {
            try
            {
                if(!await _context.Provinces.AnyAsync()
                    && !await _context.Districts.AnyAsync()
                    && !await _context.Municipalities.AnyAsync()
                    && !await _context.VDCs.AnyAsync()
                    )
                {
                    using(var connection = new SqlConnection(_configuration["ConnectionStrings:connectionString"]))
                    {
                        connection.Open();
                        using (var command = new SqlCommand("[dbo].[SpSeedData]", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
               

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
