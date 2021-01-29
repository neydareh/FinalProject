using Login.Data;
using Login.Models;
using Login.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Login.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region Actions
        public IActionResult Index()
        {

            var tickets = _context.Ticket.ToList();
            var projects = _context.Project.ToList();

            ViewBag.Pendings = tickets.Where(x => x.TicketPriority == Data.Priority.Pending).Count();
            ViewBag.Averages = tickets.Where(x => x.TicketPriority == Data.Priority.Average).Count();
            ViewBag.Lows = tickets.Where(x => x.TicketPriority == Data.Priority.Low).Count();
            ViewBag.Highs = tickets.Where(x => x.TicketPriority == Data.Priority.High).Count();
            ViewBag.Criticals = tickets.Where(x => x.TicketPriority == Data.Priority.Critical).Count();

            ViewBag.Events = tickets.Where(x => x.Type == Data.TicketType.Event).Count();
            ViewBag.Incidents = tickets.Where(x => x.Type == Data.TicketType.Incident).Count();
            ViewBag.Alerts = tickets.Where(x => x.Type == Data.TicketType.Alert).Count();
            ViewBag.Requests = tickets.Where(x => x.Type == Data.TicketType.Request).Count();

            ViewBag.Completed = tickets.Where(x => x.TicketStatus == Data.Status.Completed).Count();
            ViewBag.Open = tickets.Where(x => x.TicketStatus == Data.Status.Open).Count();
            ViewBag.Closed = tickets.Where(x => x.TicketStatus == Data.Status.Closed).Count();
            ViewBag.New = tickets.Where(x => x.TicketStatus == Data.Status.New).Count();


            ViewBag.Projects = projects;
            ViewBag.Tickets = tickets;

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }

}
