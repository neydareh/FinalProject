using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public string Content { get; set; }
        public ApplicationUser Commenter { get; set; }

        public Ticket ParentTicket { get; set; }

        public DateTime CreatedTime { get; set; }

    }
}
