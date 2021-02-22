using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Login.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Login.Repository;

namespace Login.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly IRepository _repo;
        

        public TicketsController( IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.FullName = await _repo.GetCurrentUserFullNameAsync(User);
            var tickets = _repo.GetAllTickets();
            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var projects = _repo.GetAllProjects();
            var developers = _repo.GetAllUsers();
            var ticketViewModel = new AddTicketViewModel(projects, developers);
            return View(ticketViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AddTicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticketProject = _repo.GetProject(ticketViewModel.ProjectID);
                var assignedUser = await _repo.FindUserByIdAsync(ticketViewModel.AssignedToId);
                var currentUser = await _repo.GetCurrentUser(User);
                
                // Map the ticket to the ticketViewModel 
                //TODO - use AutoMapper when refactoring
                var ticket = new Ticket
                {
                    Title = ticketViewModel.Title,
                    Description = ticketViewModel.Description,
                    TicketStatus = ticketViewModel.TicketStatus,
                    TicketPriority = ticketViewModel.TicketPriority,
                    Type = ticketViewModel.Type,
                    ParentProject = ticketProject,
                    AssignedTo = assignedUser,
                    CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}"
                };
                _repo.CreateNewTicket(ticket);
                return Redirect("/Tickets");
            }
            return View(ticketViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var ticket = _repo.GetTicket(id);
            if (ticket == null)
            {
                return Redirect("/Tickets/Index");
            }
            var projects = _repo.GetAllProjects();
            var developers = _repo.GetAllUsers();

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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AddTicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticketToEdit = _repo.GetTicket(ticketViewModel.Id);
                ticketToEdit.Title = ticketViewModel.Title;
                ticketToEdit.Description = ticketViewModel.Description;
                ticketToEdit.TicketStatus = ticketViewModel.TicketStatus;
                ticketToEdit.TicketPriority = ticketViewModel.TicketPriority;
                ticketToEdit.Type = ticketViewModel.Type;
                ticketToEdit.ProjectID = ticketViewModel.ProjectID;
                ticketToEdit.AssignedToId = ticketViewModel.AssignedToId;

                _repo.EditTicket(ticketToEdit);
                return Redirect("/Tickets");
            }
            return View(ticketViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var ticket = _repo.GetTicket(id);
            return View(ticket);
        }
        [HttpPost]
        public IActionResult Delete(Ticket ticket)
        {
            _repo.DeleteTicket(ticket);
            return Redirect("/Tickets");
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            var ticket = _repo.GetTicket(id);
            ViewBag.ParentProject = _repo.GetProject(ticket.ProjectID).Name;
            ViewBag.CreatedDate = ticket.CreatedDate;
            ViewBag.UpdatedDate = ticket.UpdatedDate;
            return View(ticket);
        }
    }
}
