using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Data
{
    public static class RoleSeeder
    {

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole { Name = "Admin" };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var userRole = new IdentityRole { Name = "User" };
                await roleManager.CreateAsync(userRole);
            }

            // Add more roles as needed
        }



        //public static void SeedRoles(IServiceProvider serviceProvider)
        //{
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    SeedRole(roleManager, "Admin");
        //    SeedRole(roleManager, "User");
        //}

        //private static void SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        //{
        //    if(!roleManager.RoleExistsAsync(roleName).Result)
        //    {
        //        var role = new IdentityRole { Name = roleName};
        //        var result = roleManager.CreateAsync(role).Result;
        //        if(!result.Succeeded)
        //        {
        //            Console.WriteLine("Role Added Sucessfully");
        //        }

        //    }
        //}
    }
}
