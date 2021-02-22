using Login.Models;
using Login.Repository;
using Login.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Login.Controllers
{
    [Route("/[controller]")]
    [Authorize]
    public class CommentsController : Controller
    {

        private readonly IRepository repo;

        public CommentsController(IRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("/[controller]/all")]
        public async Task<IActionResult> IndexAsync()
        {
            var comments = await repo.GetAllComments();
            return Ok(comments);
        }

        [HttpGet]
        [Route("/[controller]/all/{id}")]
        public async Task<IActionResult> GetCommentsByTicketId([FromRoute] int id)
        {
            
            var comments = await repo.GetAllComments(id);
            return Ok(comments);
        }


        [HttpPost]
        [Route("/[controller]/add")]
        public async Task<IActionResult> AddComment([FromBody] CommentViewModel commentoAdd)
        {
            var currentUser = await repo.GetCurrentUserFullNameAsync(User);
            var comment = new Comment 
            { 
                TicketId = commentoAdd.TicketId,
                Message = commentoAdd.Message,
                PostedBy = currentUser
            };
            var created = await repo.CreateComment(comment);
            if (created)
            {
                return Ok("Created new comment");
            }
            return NotFound("Error adding comment");
        }

        [HttpDelete]
        [Route("/[controller]/delete/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var deleted = await repo.DeleteComment(id);
            if (deleted)
            {
                return Ok($"Delete comment with id: {id}");
            }
            return NoContent();
        }

    }
}
