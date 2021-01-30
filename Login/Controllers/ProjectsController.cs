﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Login.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Login.Repository;

namespace Login.Controllers
{
    [Authorize(Roles = "Manager, Admin, SuperAdmin")]
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IRepository _repo;

        public ProjectsController(IRepository repo, ILogger<ProjectsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = _repo.GetAllProjects();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var project = new AddProjectViewModel();
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddProjectViewModel addProject)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Name = addProject.Name,
                    Description = addProject.Description
                };
                _repo.CreateNewProject(project);
                return Redirect("/Projects");
            }
            return View(addProject);
        }

        [HttpGet]
        public async Task<IActionResult> DetailAsync(int? id)
        {
            var project = _repo.GetProject(id);
            var tickets = _repo.GetAllTickets();
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
                    var fn = await _repo.FindUserByIdAsync(developer);
                    //string fullName = $"{_context.User.Find(developer).FirstName} {fn.LastName}";
                    //developersDetail.Add(fullName);
                }

                ViewBag.TicketsList = tickets;
                ViewBag.Dev = developersDetail;
            }
            ViewBag.TicketsCount = _repo.GetTicketCount(id);
            return View(project);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public IActionResult Delete(int? id)
        {
            var project = _repo.GetProject(id);
            if (project == null)
            {
                return Redirect("/404");
            }
            return View(project);
        }
        [HttpPost]
        public IActionResult Delete(Project project)
        {
            _repo.DeleteProject(project);
            return Redirect("/Projects");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var project = _repo.GetProject(id);
            var projectViewModel = new AddProjectViewModel 
            {
                ProjectID = project.ProjectID,
                Description = project.Description,
                Name = project.Name
            };
            return View(projectViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AddProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var project = _repo.GetProject(projectViewModel.ProjectID);
                project.Name = projectViewModel.Name;
                project.Description = projectViewModel.Description;

                _repo.EditProject(project);

                return Redirect("/Projects");
            }
            return View(projectViewModel);
        }


        [HttpGet]
        [Route("/404")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
