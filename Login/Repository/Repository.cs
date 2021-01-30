using Login.Data;
using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Login.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Repository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Ticket> GetAllTickets()
        {
           return _context.Ticket
                .Include(p => p.ParentProject)
                .Include(t => t.AssignedTo)
                .ToList();
        }
        public Ticket GetTicket(int? id)
        {
            return _context.Ticket.Where(t => t.Id == id).FirstOrDefault();
        }
        public void CreateNewTicket(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            _context.SaveChanges();
        }
        public void DeleteTicket(Ticket ticket)
        {
            _context.Ticket.Remove(ticket);
            _context.SaveChanges();
        }
        public void EditTicket(Ticket ticketToEdit)
        {
            _context.Ticket.Update(ticketToEdit);
            _context.SaveChanges();
        }


        public List<Project> GetAllProjects()
        {
            return _context.Project.ToList();
        }
        public Project GetProject(int id)
        {
            return _context.Project.Where(p => p.ProjectID == id).FirstOrDefault();
        }

        public void CreateNewProject(Project project)
        {
            throw new NotImplementedException();
        }


        #region User
        public List<ApplicationUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public async Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal User)
        {
            return await _userManager.GetUserAsync(User);
        }
        public async Task<string> GetCurrentUserFullNameAsync(ClaimsPrincipal User)
        {
            var user = await GetCurrentUser(User);
            return $"{user.FirstName} {user.LastName}";
        }
        public async Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        #endregion

    }
}
