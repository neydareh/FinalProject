using System.ComponentModel.DataAnnotations;

namespace Login.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int TicketId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int MainCommentId { get; set; }
        [Required]
        public string PostedBy { get; set; }
    }
}
