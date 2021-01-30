using Login.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Login.Repository
{
    public interface IRepository
    {
        #region Tickets
        public List<Ticket> GetAllTickets();
        public Ticket GetTicket(int? id);
        public void CreateNewTicket(Ticket ticket);
        public void EditTicket(Ticket ticket);
        public void DeleteTicket(Ticket ticket);
        public List<Ticket> GetTicketCount(int? id);
        #endregion

        #region Projects
        public List<Project> GetAllProjects();
        public Project GetProject(int? id);
        public void CreateNewProject(Project project);
        public void EditProject(Project project);
        public void DeleteProject(Project project);
        #endregion

        #region Users
        public List<ApplicationUser> GetAllUsers();
        public Task <ApplicationUser> GetCurrentUser(ClaimsPrincipal User);
        public Task <string> GetCurrentUserFullNameAsync(ClaimsPrincipal User);
        public Task <ApplicationUser> FindUserByIdAsync(string userId);
        #endregion
    }
}
