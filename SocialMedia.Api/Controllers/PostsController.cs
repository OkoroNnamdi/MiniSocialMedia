
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Services.SocialMediaServices;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly CreatePostService _createPostService;
        

        public PostsController(CreatePostService createPostService)
        {
            _createPostService = createPostService;
           
        }

        [HttpPost("Post")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            try
            {
                var post = await _createPostService.CreatePostAsync(request.UserId, request.Content);
                return Ok(post);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log and handle unexpected exceptions
                return StatusCode(500, new { Error = "An unexpected error occurred." });
            }
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetPostFeed([FromQuery] Guid userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var posts = await _createPostService.GetPostAsync(userId, pageNumber, pageSize);
                return Ok(posts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log and handle unexpected exceptions
                return StatusCode(500, new { Error = "An unexpected error occurred." });
            }
        }
    }
}


