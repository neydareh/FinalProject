using Login.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Login.Utilities
{
    public static class UtilityLib
    {
        public static async Task<IEnumerable<string>> GetUserRoles(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }

    }
}
