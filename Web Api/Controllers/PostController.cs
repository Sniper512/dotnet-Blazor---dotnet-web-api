using Microsoft.AspNetCore.Mvc;
using Web_Api.Models;
using Web_Api.Repositories;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository repo, ILogger<PostController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = await _repo.GetAllItemsAsync();
                if (posts == null || !posts.Any())
                {
                    return NotFound("No posts found.");
                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all posts");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _repo.GetItemByIdAsync(id);
                if (post == null)
                {
                    return NotFound($"Post with ID {id} not found.");
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching post with ID {PostId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post newPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _repo.AddItemAsync(newPost);
                if (success)
                {
                    return CreatedAtAction(nameof(GetPost), new { id = newPost.Id }, newPost);
                }
                return BadRequest("Failed to create the post.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new post");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post updatedPost)
        {
            if (id != updatedPost.Id)
            {
                return BadRequest("ID in URL does not match ID in the request body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _repo.UpdateItemAsync(updatedPost);
                if (success)
                {
                    return NoContent();
                }
                return NotFound($"Post with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating post with ID {PostId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var success = await _repo.DeleteItemAsync(id);
                if (success)
                {
                    return NoContent();
                }
                return NotFound($"Post with ID {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting post with ID {PostId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetPostCount()
        {
            try
            {
                var count = await _repo.GetCountAsync();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching post count");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}