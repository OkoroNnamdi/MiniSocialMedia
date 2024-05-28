using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;
using SocialMedia.Services.SocialMediaServices;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FollowUserServices _followUserService;
        private readonly CreateUserService _createUserService;
        private readonly IMapper _mapper;
        public UsersController(FollowUserServices followUserService, CreateUserService createUserService,IMapper mapper)
        {
           _followUserService = followUserService;
            _createUserService = createUserService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = await _createUserService.ExecuteAsync(request.Username, request.Email, request.Password);
                // var userDto = _mapper.Map<UserDto>(user);
                var userDto = new UserDto
                {
                    Username =request.Username,
                    Email =request.Email,
                    Password =request.Password
                };
                return Ok(userDto);
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
        [HttpPost("follow")]
        public async Task<IActionResult> FollowUser([FromBody] FollowUserRequest request)
        {
            try
            {
                await _followUserService.FollowUserAsync(request.FollowerId, request.FolloweeId);
                return Ok();
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
