using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyAssignmentManager.API.Services;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Infrastructure;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;
        private readonly UserService _userService;

        public AuthController(Context context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _userService = new UserService(_userRepository);
        }

        // POST: api/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest model)
        {
            try
            {
                var response = await _userService.Login(model);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        // POST: api/register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(RegisterUserDto model)
        {
            try
            {
                var response = await _userService.Register(model);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
