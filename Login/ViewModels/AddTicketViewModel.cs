using Login.Data;
using Login.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Login.ViewModels
{
    public class AddTicketViewModel : BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }


        [Required(ErrorMessage = "Ticket title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ticket description is required")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Ticket status is required")]
        public Status TicketStatus { get; set; }
        public List<SelectListItem> Status { get; set; } = new List<SelectListItem> 
        {
            new SelectListItem(Data.Status.Open.ToString(), ((int)Data.Status.Open).ToString()),
            new SelectListItem(Data.Status.Closed.ToString(), ((int)Data.Status.Closed).ToString()),
            new SelectListItem(Data.Status.Completed.ToString(), ((int)Data.Status.Completed).ToString()),
            new SelectListItem(Data.Status.New.ToString(), ((int)Data.Status.New).ToString())
        };

        [Required(ErrorMessage = "Ticket priority type is required")]
        public Priority TicketPriority { get; set; }
        public List<SelectListItem> Priority { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(Data.Priority.Pending.ToString(), ((int)Data.Priority.Pending).ToString()),
            new SelectListItem(Data.Priority.Low.ToString(), ((int)Data.Priority.Low).ToString()),
            new SelectListItem(Data.Priority.Average.ToString(), ((int)Data.Priority.Average).ToString()),
            new SelectListItem(Data.Priority.High.ToString(), ((int)Data.Priority.High).ToString()),
            new SelectListItem(Data.Priority.Critical.ToString(), ((int)Data.Priority.Critical).ToString())
        };

        [Required(ErrorMessage = "Ticket type is required")]
        public TicketType Type { get; set; }
        public List<SelectListItem> TicketType { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(Data.TicketType.Event.ToString(), ((int)Data.TicketType.Event).ToString()),
            new SelectListItem(Data.TicketType.Request.ToString(), ((int)Data.TicketType.Request).ToString()),
            new SelectListItem(Data.TicketType.Alert.ToString(), ((int)Data.TicketType.Alert).ToString()),
            new SelectListItem(Data.TicketType.Incident.ToString(), ((int)Data.TicketType.Incident).ToString()),
        };


        [Required(ErrorMessage = "Project is required")]
        public int ProjectID { get; set; }
        public List<SelectListItem> Projects { get; set; }


        [Required(ErrorMessage = "Please assign ticket to Developer")]
        public string AssignedToId { get; set; }
        public List<SelectListItem> Users { get; set; }


        public AddTicketViewModel(List<Project> projects, List<ApplicationUser> developers)
        {
            Projects = new List<SelectListItem>();
            Users = new List<SelectListItem>();

            foreach (var developer in developers)
            {
                Users.Add(new SelectListItem
                {
                    Value = developer.Id,
                    Text = $"{developer.FirstName} {developer.LastName}"
                });
            }
            foreach (var project in projects)
            {
                Projects.Add(new SelectListItem
                {
                    Value = project.ProjectID.ToString(),
                    Text = project.Name
                });
            }
        }

        public AddTicketViewModel()
        {

        }
    }
}
