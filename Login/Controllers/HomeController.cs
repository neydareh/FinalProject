using Login.Models;
using Login.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Login.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repo;
        
        public HomeController(ILogger<HomeController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        #region Actions
        public IActionResult Index()
        {

            ViewBag.Tickets = _repo.GetAllTickets();
            ViewBag.Projects = _repo.GetAllProjects();

            ViewBag.Pendings = _repo.GetCountOfPendingTickets();
            ViewBag.Averages = _repo.GetCountOfAvgerageTickets();
            ViewBag.Lows = _repo.GetCountOfLowTickets();
            ViewBag.Highs = _repo.GetCountOfHighTickets();
            ViewBag.Criticals = _repo.GetCountOfCriticalTickets();

            ViewBag.Events = _repo.GetCountOfEventTickets();
            ViewBag.Incidents = _repo.GetCountOfIncidentTickets();
            ViewBag.Alerts = _repo.GetCountOfAlertTickets();
            ViewBag.Requests = _repo.GetCountOfRequestTickets();

            ViewBag.Completed = _repo.GetCountOfCompletedTickets();
            ViewBag.Open = _repo.GetCountOfOpenTickets();
            ViewBag.Closed = _repo.GetCountOfClosedTickets();
            ViewBag.New = _repo.GetCountOfNewTickets();

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
