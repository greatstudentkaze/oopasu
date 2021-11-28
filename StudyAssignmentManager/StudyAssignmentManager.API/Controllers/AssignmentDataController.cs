using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Infrastructure;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentDataController : ControllerBase
    {
        private readonly Context _context;
        private readonly AssignmentDataRepository _assignmentDataRepository;

        public AssignmentDataController(Context context)
        {
            _context = context;
            _assignmentDataRepository = new AssignmentDataRepository(_context);
        }

        // GET: api/AssignmentData/author/:id
        [HttpGet("author/{id}")]
        public async Task<ActionResult<IEnumerable<AssignmentData>>> GetAssignmentDataListByAuthorId(Guid id)
        {
            return await _assignmentDataRepository.GetByAuthorIdAsync(id);
        }

        // GET: api/AssignmentData/:id/assignments
        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<IEnumerable<StudyAssignment>>> GetStudyAssignmentsByDataId(Guid id)
        {
            var assignmentData = await _assignmentDataRepository.GetByIdAsync(id);

            if (assignmentData == null)
            {
                return NotFound();
            }

            return assignmentData.Assignments;
        }

        // GET: api/AssignmentData/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentData>> GetAssignmentData(Guid id)
        {
            var assignmentData = await _assignmentDataRepository.GetByIdAsync(id);

            if (assignmentData == null)
            {
                return NotFound();
            }

            return assignmentData;
        }

        // PUT: api/AssignmentData/:id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignmentData(Guid id, AssignmentData assignmentData)
        {
            if (id != assignmentData.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _assignmentDataRepository.UpdateAsync(assignmentData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_assignmentDataRepository.EntryExists(id))
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

        // POST: api/AssignmentData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssignmentData>> PostAssignmentData(AssignmentData assignmentData)
        {
            await _assignmentDataRepository.AddAsync(assignmentData);
            return CreatedAtAction(nameof(GetAssignmentData), new { id = assignmentData.Id }, assignmentData);
        }

        // DELETE: api/AssignmentData/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignmentData(Guid id)
        {
            await _assignmentDataRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
