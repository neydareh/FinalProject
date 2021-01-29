using Login.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Ticket : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Status TicketStatus { get; set; }
        public Priority TicketPriority { get; set; }
        public TicketType Type { get; set; }

        public int ProjectID { get; set; }
        public Project ParentProject { get; set; }
        [MaxLength(450)]
        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
    }
}
