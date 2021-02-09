using System;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public string PostedBy { get; set; }

    }
}
