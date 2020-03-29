using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Todo
{
    public static class SeedData
    {
        public static async Task InitializeAsyc(IServiceProvider services)
        {

            //var role = new RoleManager(Constants.AdministratorRole);

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));


            // var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            // var result = await _userManager.CreateAsync(user, Input.Password);

            // var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // await EnsureRolesAsync(roleManager);

            // var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            // await EnsureTestAdminAsync(userManager);

        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExist = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

            if(alreadyExist) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(
            UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@todo.local")
                .SingleOrDefaultAsync();
            
            if (testAdmin != null) return;

            testAdmin = new IdentityUser
            {
                UserName = "admin@todo.local",
                Email = "admin@todo.local"
            };

            await userManager.CreateAsync(testAdmin, "Password123!");
            
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    }
}