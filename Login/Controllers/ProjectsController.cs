using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login.Data;
using Login.Models;
using Login.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Login.Controllers
{
    [Authorize(Roles = "Manager, Admin, SuperAdmin")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ApplicationDbContext context, ILogger<ProjectsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Project.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var project = new AddProjectViewModel();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProjectViewModel addProject)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Name = addProject.Name,
                    Description = addProject.Description
                };
                _context.Add(project);
                await _context.SaveChangesAsync();
                return Redirect("/Projects");
            }
            return View(addProject);
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            var project = _context.Project.Where(p => p.ProjectID == id).FirstOrDefault();
            var tickets = _context.Ticket.Where(t => t.ProjectID == id).ToList();
            if (tickets.Count < 1)
            {
                ViewBag.Dev = null;
                ViewBag.TicketsList = null;
            }
            else
            {
                var developersId = tickets.Select(x => x.AssignedToId).Distinct().ToList();
                var developersDetail = new List<string>();
                foreach (var developer in developersId)
                {
                    string fullName = $"{_context.Users.Find(developer).FirstName} {_context.Users.Find(developer).LastName}";
                    developersDetail.Add(fullName);
                }

                ViewBag.TicketsList = tickets;
                ViewBag.Dev = developersDetail;
            }
            ViewBag.TicketsCount = _context.Ticket.Where(t => t.ProjectID == id).ToList();
            return View(project);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public IActionResult Delete(int? id)
        {
            var project = _context.Project.Find(id);
            if (project == null)
            {
                return Redirect("/404");
            }
            return View(project);
        }

        [HttpGet]
        [Route("/404")]
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Project project)
        {
            _context.Remove(project);
            await _context.SaveChangesAsync();
            return Redirect("/Projects");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var project = _context.Project.Find(id);
            var projectViewModel = new AddProjectViewModel 
            {
                ProjectID = project.ProjectID,
                Description = project.Description,
                Name = project.Name
            };
            return View(projectViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var project = _context.Project
                    .Where(x => x.ProjectID == projectViewModel.ProjectID)
                    .FirstOrDefault();
                project.Name = projectViewModel.Name;
                project.Description = projectViewModel.Description;

                _context.Update(project);
                _context.SaveChanges();

                return Redirect("/Projects");
            }
            return View(projectViewModel);
        }
    }
}
