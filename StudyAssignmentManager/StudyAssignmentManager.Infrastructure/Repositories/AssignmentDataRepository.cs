using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public class AssignmentDataRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        
        public AssignmentDataRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<AssignmentData> GetByIdAsync(Guid id)
        {
            return await _context.AssignmentDatas.FindAsync(id);
        }
        
        public async Task<List<AssignmentData>> GetByAuthorIdAsync(Guid id)
        {
            return await _context.AssignmentDatas.Where(it => it.AuthorId == id).ToListAsync();
        }
        
        public async Task AddAsync(AssignmentData studyAssignment)
        {
            _context.AssignmentDatas.Add(studyAssignment);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(AssignmentData studyAssignment)
        {
            var existAnswer = await _context.Answers.FindAsync(studyAssignment.Id);
            _context.Entry(existAnswer).CurrentValues.SetValues(studyAssignment);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var studyAssignment = await _context.AssignmentDatas.FindAsync(id);
            _context.Remove(studyAssignment);
            await _context.SaveChangesAsync();
        }

        public bool EntryExists(Guid id)
        {
            return _context.AssignmentDatas.Any(e => e.Id == id);
        }
    }
}