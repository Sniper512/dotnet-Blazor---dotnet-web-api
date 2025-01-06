using Microsoft.AspNetCore.Mvc;
using Web_Api.Models;
using Web_Api.Repositories;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentRepository repo, ILogger<CommentController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCommentsForPost(int postId)
        {
            try
            {
                var comments = await _repo.GetAllCommentsForAPostAsync(postId);
                if (comments == null || !comments.Any())
                {
                    return NotFound($"No comments found for post with ID {postId}.");
                }
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching comments for post with ID {PostId}", postId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment newComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _repo.AddCommentAsync(newComment);
                if (success)
                {
                    return CreatedAtAction(nameof(GetCommentsForPost), new { postId = newComment.PostId }, newComment);
                }
                return BadRequest("Failed to create the comment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new comment");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment updatedComment)
        {
            if (id != updatedComment.Id)
            {
                return BadRequest("ID in URL does not match ID in the request body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _repo.UpdateCommentAsync(updatedComment);
                if (success)
                {
                    return NoContent();
                }
                return NotFound($"Comment with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating comment with ID {CommentId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var success = await _repo.DeleteCommentAsync(id);
                if (success)
                {
                    return NoContent();
                }
                return NotFound($"Comment with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting comment with ID {CommentId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikeComment(int id)
        {
            try
            {
                var success = await _repo.CommentLikeAsync(id);
                if (success)
                {
                    return Ok(new { message = "Comment liked successfully." });
                }
                return NotFound($"Comment with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while liking comment with ID {CommentId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCommentCount()
        {
            try
            {
                var count = await _repo.GetCommentsCountAsync();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching comment count");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}