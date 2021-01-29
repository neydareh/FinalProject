using Login.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Login.Utilities
{
    public static class UtilityLib
    {
        public static List<Ticket> GetProjectTicket(int projectId)
        {
            throw new NotImplementedException();
        }
        public static async Task<string> GetCurrentUserRoleAsync(UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {
            var user = await userManager.GetUserAsync(User);
            var roles = await userManager.GetRolesAsync(user);

            if (roles.Count == 1)
            {
               return roles.FirstOrDefault();
            }
            return "";
        }
        public static async Task<IEnumerable<string>> GetUserRoles(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }

    }
}
