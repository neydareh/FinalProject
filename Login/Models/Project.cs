using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
