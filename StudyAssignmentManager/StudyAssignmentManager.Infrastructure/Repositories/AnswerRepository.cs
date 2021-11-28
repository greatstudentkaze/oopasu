using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        
        public AnswerRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<Answer> GetByIdAsync(Guid id)
        {
            return await _context.Answers.FindAsync(id);
        }
        
        public async Task<List<Answer>> GetByAssignmentIdAsync(Guid assignmentId)
        {
            return await _context.Answers.Where(it => it.AssignmentId == assignmentId).ToListAsync();
        }
        
        public async Task AddAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Answer answer)
        {
            var existAnswer = await _context.Answers.FindAsync(answer.Id);
            _context.Entry(existAnswer).CurrentValues.SetValues(answer);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddCommentAsync(Guid id, AddCommentDto model)
        {
            var existAnswer = await _context.Answers.FindAsync(id);
            var updatedComments = existAnswer.Comments.ToList();
            updatedComments.Add(model.Comment);
            _context.Entry(existAnswer).CurrentValues.SetValues(new { Comments = updatedComments });
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Answer answer)
        {
            _context.Remove(answer);
            await _context.SaveChangesAsync();
        }

        public bool EntryExists(Guid id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}