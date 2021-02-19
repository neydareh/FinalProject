using Login.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;


namespace Login.Data
{
    public class ContextSeed
    {
        public static async Task SeedRoleAsync (RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles into Database
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }

        // This should not go into production, only use once to seed SuperAdmin user to the database in test
        public static async Task SeedSuperAdminAsync (UserManager<ApplicationUser> userManager, string superAdminPassword)
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

            //Check if there is a SuperAdmin SuperAdmin is currently in the Database
            if (userManager.Users.All(u => u.Id != defaultUser.Id ))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                //If there's no SuperAdmin then create one using the IdentityUser details provided above
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, superAdminPassword);
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    /**
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Developer.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());**/
                }
            }
        }
    }
}
