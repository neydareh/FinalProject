using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public string PostedBy { get; set; }

        [ForeignKey(nameof(TicketId))]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

    }
}
