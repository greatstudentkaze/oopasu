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
        private readonly EducationalMaterialRepository _educationalMaterialRepository;

        public EducationalMaterialController(Context context)
        {
            _context = context;
            _educationalMaterialRepository = new EducationalMaterialRepository(_context);
        }

        // GET: api/educational-materials/author/:id
        [HttpGet("author/{id}")]
        public async Task<ActionResult<IEnumerable<EducationalMaterial>>> GetEducationalMaterialListByAuthorId(Guid id)
        {
            return await _educationalMaterialRepository.GetByAuthorIdAsync(id);
        }

        // GET: api/educational-materials/:id/assignments
        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<IEnumerable<StudyAssignment>>> GetStudyAssignmentsByDataId(Guid id)
        {
            var educationalMaterial = await _educationalMaterialRepository.GetByIdAsync(id);

            if (educationalMaterial == null)
            {
                return NotFound();
            }

            return educationalMaterial.Assignments;
        }

        // GET: api/educational-materials/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalMaterialDto>> GetEducationalMaterial(Guid id)
        {
            var educationalMaterial = await _educationalMaterialRepository.GetByIdAsync(id);

            if (educationalMaterial == null)
            {
                return NotFound();
            }

            return ItemToDTO(educationalMaterial);
        }

        // PUT: api/educational-materials/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducationalMaterial(Guid id, EducationalMaterial educationalMaterial)
        {
            if (id != educationalMaterial.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _educationalMaterialRepository.UpdateAsync(educationalMaterial);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_educationalMaterialRepository.EntryExists(id))
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
        [HttpPost]
        public async Task<ActionResult<EducationalMaterial>> PostEducationalMaterial(EducationalMaterial educationalMaterial)
        {
            await _educationalMaterialRepository.AddAsync(educationalMaterial);
            return CreatedAtAction(nameof(GetEducationalMaterial), new { id = educationalMaterial.Id }, ItemToDTO(educationalMaterial));
        }

        // DELETE: api/educational-materials/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducationalMaterial(Guid id)
        {
            await _educationalMaterialRepository.DeleteAsync(id);
            return NoContent();
        }
        
        private static EducationalMaterialDto ItemToDTO(EducationalMaterial educationalMaterial) =>
            new()
            {
                Id = educationalMaterial.Id,
                Title = educationalMaterial.Title,
                Description = educationalMaterial.Description, 
                Content = educationalMaterial.Content,
                AuthorId = educationalMaterial.AuthorId,
            };
    }
}
