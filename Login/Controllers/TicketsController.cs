using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login.Data;
using Login.Models;
using Login.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Login.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var fullName = $"{user.FirstName} {user.LastName}";
            ViewBag.FullName = fullName;
            var tickets = await _context.Ticket
                .Include(p => p.ParentProject)
                .Include(u => u.AssignedTo)
                .ToListAsync();

            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var projects = _context.Project.ToList();
            var developers = _context.Users.ToList();
            var ticketViewModel = new AddTicketViewModel(projects, developers);
            return View(ticketViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AddTicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticketProject = _context.Project.Find(ticketViewModel.ProjectID);
                var assignedUser = await _userManager.FindByIdAsync(ticketViewModel.AssignedToId);
                var currentUser = await _userManager.GetUserAsync(User);

                var ticket = new Ticket
                {
                    Title = ticketViewModel.Title,
                    Description = ticketViewModel.Description,
                    //CreatedDate = ticketViewModel.CreatedDate,
                    TicketStatus = ticketViewModel.TicketStatus,
                    TicketPriority = ticketViewModel.TicketPriority,
                    Type = ticketViewModel.Type,
                    ParentProject = ticketProject,
                    AssignedTo = assignedUser,
                    CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}"
                };

                _context.Ticket.Add(ticket);
                _context.SaveChanges();

                return Redirect("/Tickets");
            }
            return View(ticketViewModel);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var ticket = _context.Ticket.Where(t => t.Id == id).FirstOrDefault();
            if (ticket == null)
            {
                return Redirect("/Tickets/Index");
            }
            var projects = _context.Project.ToList();
            var developers = _context.Users.ToList();
            var ticketViewModel = new AddTicketViewModel(projects, developers)
            {
                Title = ticket.Title,
                Description = ticket.Description,
                CreatedDate = ticket.CreatedDate,
                TicketStatus = ticket.TicketStatus,
                TicketPriority = ticket.TicketPriority,
                Type = ticket.Type,
                CreatedBy = ticket.CreatedBy,
                ProjectID = ticket.ProjectID,
                AssignedToId = ticket.AssignedToId
            };
            return View(ticketViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddTicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticketToEdit = _context.Ticket.Where( t => t.Id == ticketViewModel.Id).FirstOrDefault();
                ticketToEdit.Title = ticketViewModel.Title;
                ticketToEdit.Description = ticketViewModel.Description;
                ticketToEdit.TicketStatus = ticketViewModel.TicketStatus;
                ticketToEdit.TicketPriority = ticketViewModel.TicketPriority;
                ticketToEdit.Type = ticketViewModel.Type;
                ticketToEdit.ProjectID = ticketViewModel.ProjectID;
                ticketToEdit.AssignedToId = ticketViewModel.AssignedToId;

                

                _context.Ticket.Update(ticketToEdit);
                _context.SaveChanges();

                return Redirect("/Tickets");
            }
            return View(ticketViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var ticket = _context.Ticket.Find(id);
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Ticket ticket)
        {
            _context.Remove(ticket);
            await _context.SaveChangesAsync();
            return Redirect("/Tickets");
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            var ticket = _context.Ticket.Find(id);
            ViewBag.ParentProject = _context.Project.Find(ticket.ProjectID).Name;
            ViewBag.CreatedDate = ticket.CreatedDate;
            ViewBag.UpdatedDate = ticket.UpdatedDate;
            return View(ticket);
        }
    }
}
