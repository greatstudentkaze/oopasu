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
    [Route("api/educational-materials")]
    [ApiController]
    public class EducationalMaterialController : ControllerBase
    {
        private readonly Context _context;
        private readonly EducationalMaterialRepository _assignmentDataRepository;

        public EducationalMaterialController(Context context)
        {
            _context = context;
            _assignmentDataRepository = new EducationalMaterialRepository(_context);
        }

        // GET: api/educational-materials/author/:id
        [HttpGet("author/{id}")]
        public async Task<ActionResult<IEnumerable<EducationalMaterial>>> GetEducationalMaterialListByAuthorId(Guid id)
        {
            return await _assignmentDataRepository.GetByAuthorIdAsync(id);
        }

        // GET: api/educational-materials/:id/assignments
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

        // GET: api/educational-materials/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalMaterial>> GetEducationalMaterial(Guid id)
        {
            var assignmentData = await _assignmentDataRepository.GetByIdAsync(id);

            if (assignmentData == null)
            {
                return NotFound();
            }

            return assignmentData;
        }

        // PUT: api/educational-materials/:id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationalMaterial(Guid id, EducationalMaterial assignmentData)
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

        // POST: api/educational-materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationalMaterial>> PostEducationalMaterial(EducationalMaterial assignmentData)
        {
            await _assignmentDataRepository.AddAsync(assignmentData);
            return CreatedAtAction(nameof(GetEducationalMaterial), new { id = assignmentData.Id }, assignmentData);
        }

        // DELETE: api/educational-materials/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducationalMaterial(Guid id)
        {
            await _assignmentDataRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
