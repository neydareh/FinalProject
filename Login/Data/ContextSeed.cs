using Login.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;


namespace Login.Data
{
    public class ContextSeed
    {
        public static async Task SeedRoleAsync (UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles into Database
            await roleManager.CreateAsync(new IdentityRole(Data.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Data.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Data.Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Data.Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Data.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync (UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, string superAdminPassword)
        {
            //Seed SuperAdmin User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@app.com",
                FirstName = "Olakunle",
                LastName = "Neye",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            //Check if the SuperAdmin is current in the Database
            if (userManager.Users.All(u => u.Id != defaultUser.Id ))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, superAdminPassword);
                    await userManager.AddToRoleAsync(defaultUser, Data.Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Data.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Data.Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Data.Roles.Developer.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Data.Roles.Basic.ToString());
                }
            }
        }
    }
}
