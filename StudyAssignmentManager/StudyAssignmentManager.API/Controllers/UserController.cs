using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Infrastructure;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;

        public UserController(Context context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        // DELETE: api/users/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userRepository.DeleteAsync(id);
            return NoContent();
        }

        // PATCH: api/users/:id
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCheckRequest(Guid id, JsonPatchDocument<User> userUpdates)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            
            userUpdates.ApplyTo(user);
            await _userRepository.UpdateAsync(user);

            return NoContent();
        }
    }
}
