using System.ComponentModel.DataAnnotations;

namespace Login.ViewModels
{
    public class AddProjectViewModel
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Project name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Project description is necessary")]
        public string Description { get; set; }
    }
}
