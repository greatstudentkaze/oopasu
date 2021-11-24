using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.API.Services;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Infrastructure;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;
        private readonly UserService _userService;

        public UserController(Context context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _userService = new UserService(_userRepository);
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest model)
        {
            var response = await _userService.Login(model);
            if (response == null)
            {
                return BadRequest(new {Message = "Неверный email или пароль!"});
            }

            return Ok(response);
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(RegisterUserDto model)
        {
            var response = await _userService.Register(model);
            if (response == null)
            {
                return BadRequest(new {Message = "Возникла ошибка при регистрации"});
            }

            return Ok(response);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
