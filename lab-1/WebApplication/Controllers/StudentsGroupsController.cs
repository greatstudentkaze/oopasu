using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsGroupsController : ControllerBase
    {
        private readonly StudentsContext _context;

        public StudentsGroupsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: api/StudentsGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentsGroupDTO>>> GetStudentsGroups()
        {
            return await _context.StudentsGroups
                .Select(it => ItemToDTO(it))
                .ToListAsync();
        }

        // GET: api/StudentsGroups/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentsGroupDTO>> GetStudentsGroup(Guid id)
        {
            var studentsGroup = await _context.StudentsGroups.FindAsync(id);

            if (studentsGroup == null) return NotFound();

            return ItemToDTO(studentsGroup);
        }

        // PUT: api/StudentsGroups/:id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentsGroup(Guid id, StudentsGroup studentsGroup)
        {
            if (id != studentsGroup.Id) return BadRequest();

            _context.Entry(studentsGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id)) return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/StudentsGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentsGroup>> PostStudentsGroup(StudentsGroup studentsGroup)
        {
            if (EntryExists(studentsGroup.Id) || NotGraduatedGroupExists(studentsGroup.Number)) return BadRequest();

            _context.StudentsGroups.Add(studentsGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentsGroup), new {id = studentsGroup.Id}, ItemToDTO(studentsGroup));
        }

        // DELETE: api/StudentsGroups/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentsGroup(Guid id)
        {
            var studentsGroup = await _context.StudentsGroups.FindAsync(id);
            if (studentsGroup == null) return NotFound();

            _context.StudentsGroups.Remove(studentsGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(Guid id)
        {
            return _context.StudentsGroups.Any(it => it.Id == id);
        }

        private bool NotGraduatedGroupExists(string groupNumber)
        {
            return _context.StudentsGroups
                .Where(it => it.Number == groupNumber)
                .Any(it => !it.IsGraduated);
        }

        private static StudentsGroupDTO ItemToDTO(StudentsGroup item)
        {
            return new StudentsGroupDTO()
            {
                Id = item.Id,
                Course = item.Course,
                Department = item.Department,
                GraduatingDepartment = item.GraduatingDepartment,
                GroupLeaderFullName = item.GroupLeaderFullName,
                IsGraduated = item.IsGraduated,
                Number = item.Number,
                StudentsCount = item.StudentsCount,
            };
        }
    }
}