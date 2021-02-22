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
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Ticket
        public List<Ticket> GetAllTickets()
        {
           return _context.Ticket
                .Include(p => p.ParentProject)
                .Include(t => t.AssignedTo)
                .ToList();
        }
        public Ticket GetTicket(int? id)
        {
            return _context.Ticket
                .Include(t => t.Comments)
                .FirstOrDefault(t => t.Id == id);
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
        public List<Ticket> GetTicketCount(int? id)
        {
            return _context.Ticket.Where(t => t.ProjectID == id).ToList();
        }
        public int GetCountOfPendingTickets()
        {
            return GetAllTickets().Where(x => x.TicketPriority == Priority.Pending).Count();
        }
        public int GetCountOfAvgerageTickets()
        {
            return GetAllTickets().Where(x => x.TicketPriority == Priority.Average).Count();
        }
        public int GetCountOfLowTickets()
        {
            return GetAllTickets().Where(x => x.TicketPriority == Priority.Low).Count();
        }
        public int GetCountOfHighTickets()
        {
            return GetAllTickets().Where(x => x.TicketPriority == Priority.High).Count();
        }
        public int GetCountOfCriticalTickets()
        {
            return GetAllTickets().Where(x => x.TicketPriority == Priority.Critical).Count();
        }
        public int GetCountOfEventTickets()
        {
            return GetAllTickets().Where(x => x.Type == TicketType.Event).Count();
        }
        public int GetCountOfIncidentTickets()
        {
            return GetAllTickets().Where(x => x.Type == TicketType.Incident).Count();
        }
        public int GetCountOfAlertTickets()
        {
            return GetAllTickets().Where(x => x.Type == TicketType.Alert).Count();
        }
        public int GetCountOfRequestTickets()
        {
            return GetAllTickets().Where(x => x.Type == TicketType.Request).Count();
        }
        public int GetCountOfCompletedTickets()
        {
            return GetAllTickets().Where(x => x.TicketStatus == Status.Completed).Count();
        }
        public int GetCountOfOpenTickets()
        {
            return GetAllTickets().Where(x => x.TicketStatus == Status.Open).Count();
        }
        public int GetCountOfClosedTickets()
        {
            return GetAllTickets().Where(x => x.TicketStatus == Status.Closed).Count();
        }
        public int GetCountOfNewTickets()
        {
            return GetAllTickets().Where(x => x.TicketStatus == Status.New).Count();
        }
        #endregion
        #region Project
        public List<Project> GetAllProjects()
        {
            return _context.Project.ToList();
        }
        public Project GetProject(int? id)
        {
            return _context.Project.Where(p => p.ProjectID == id).FirstOrDefault();
        }
        public void CreateNewProject(Project project)
        {
            _context.Project.Add(project);
            _context.SaveChanges();
        }
        public void DeleteProject(Project project)
        {
            _context.Project.Remove(project);
            _context.SaveChanges();
        }
        public void EditProject(Project projectToEdit)
        {
            _context.Project.Update(projectToEdit);
            _context.SaveChanges();
        }
        #endregion
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

        #region Comment
        public async Task<bool> CreateComment(Comment comment)
        {
            if(comment == null)
            {
                return false;
                
            }
            comment.CreatedTime = DateTime.Now;
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return false;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAllComments(int ticketId)
        {
            return await _context.Comments.Where(c => c.TicketId == ticketId).ToListAsync();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        #endregion

    }
}
