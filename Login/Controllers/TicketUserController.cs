using Login.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Login.Controllers
{
    [Authorize]
    public class TicketUserController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TicketUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Actions
        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _context.Ticket
                .Include(t => t.AssignedTo)
                .ToList();

            return View(tickets);
        }
        #endregion
    }
}
