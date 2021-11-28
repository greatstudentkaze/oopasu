using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyAssignmentsController : ControllerBase
    {
        private readonly IStudyAssignmentRepository _studyAssignmentRepository;

        public StudyAssignmentsController(IStudyAssignmentRepository studyAssignmentRepository)
        {
            _studyAssignmentRepository = studyAssignmentRepository;
        }

        // GET: api/StudyAssignments/data/:id
        [HttpGet("data/{id}")]
        public async Task<ActionResult<IEnumerable<StudyAssignment>>> GetStudyAssignmentsByDataId(Guid id)
        {
            return await _studyAssignmentRepository.GetByDataIdAsync(id);
        }

        // GET: api/StudyAssignments/student/:id
        [HttpGet("student/{id}")]
        public async Task<ActionResult<IEnumerable<StudyAssignment>>> GetStudyAssignmentsByStudentId(Guid id)
        {
            return await _studyAssignmentRepository.GetByStudentIdAsync(id);
        }

        // GET: api/StudyAssignments/teacher/:id
        [HttpGet("teacher/{id}")]
        public async Task<ActionResult<IEnumerable<StudyAssignment>>> GetStudyAssignmentsByTeacherId(Guid id)
        {
            return await _studyAssignmentRepository.GetByTeacherIdAsync(id);
        }

        // GET: api/StudyAssignments/tutor/:id
        [HttpGet("tutor/{id}")]
        public async Task<ActionResult<IEnumerable<StudyAssignment>>> GetStudyAssignmentsByTutorId(Guid id)
        {
            return await _studyAssignmentRepository.GetByTutorIdAsync(id);
        }

        // GET: api/StudyAssignments/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudyAssignment>> GetStudyAssignment(Guid id)
        {
            var studyAssignment = await _studyAssignmentRepository.GetByIdAsync(id);
            if (studyAssignment == null)
            {
                return NotFound();
            }

            return studyAssignment;
        }
        
        // PUT: api/StudyAssignments/:id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudyAssignment(Guid id, StudyAssignment studyAssignment)
        {
            if (id != studyAssignment.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _studyAssignmentRepository.UpdateAsync(studyAssignment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_studyAssignmentRepository.EntryExists(id))
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

        // POST: api/StudyAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudyAssignment>> PostStudyAssignment(StudyAssignment studyAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            studyAssignment.CreationDate = DateTime.Now;
            await _studyAssignmentRepository.AddAsync(studyAssignment);

            return CreatedAtAction(nameof(GetStudyAssignment), new { id = studyAssignment.Id }, studyAssignment);
        }

        // DELETE: api/StudyAssignments/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudyAssignment(Guid id)
        {
            await _studyAssignmentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
