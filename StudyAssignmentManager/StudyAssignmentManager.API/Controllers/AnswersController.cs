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
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswersController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        // GET: api/Answers/assignment/:id
        [HttpGet("assignment/{id}")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersByAssignmentId(Guid id)
        {
            return await _answerRepository.GetByAssignmentIdAsync(id);
        }

        // GET: api/Answers/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(Guid id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        // PUT: api/Answers/:id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            try
            {
                await _answerRepository.UpdateAsync(answer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_answerRepository.EntryExists(id))
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

        // POST: api/Answers/:id/comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/comments")]
        public async Task<ActionResult<Answer>> AddComment(Guid id, AddCommentDto model)
        {
            await _answerRepository.AddCommentAsync(id, model);

            return Ok();
        }

        // DELETE: api/Answers/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            
            await _answerRepository.DeleteAsync(answer);
            return NoContent();
        }
    }
}
