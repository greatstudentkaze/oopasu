using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public class StudyAssignmentRepository : IStudyAssignmentRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        
        public StudyAssignmentRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<StudyAssignment> GetByIdAsync(Guid id)
        {
            return await _context.StudyAssignments.FindAsync(id);
        }
        
        public async Task<List<StudyAssignment>> GetByTutorIdAsync(Guid id)
        {
            return await _context.StudyAssignments.Where(it => it.TutorId == id).ToListAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByTeacherIdAsync(Guid id)
        {
            return await _context.StudyAssignments.Where(it => it.TeacherId == id).ToListAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByStudentIdAsync(Guid id)
        {
            return await _context.StudyAssignments.Where(it => it.StudentId == id).ToListAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByDataIdAsync(Guid id)
        {
            return await _context.StudyAssignments.Where(it => it.DataId == id).ToListAsync();
        }
        
        public async Task AddAsync(StudyAssignment studyAssignment)
        {
            _context.StudyAssignments.Add(studyAssignment);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(StudyAssignment studyAssignment)
        {
            var existStudyAssignment = await _context.StudyAssignments.FindAsync(studyAssignment.Id);
            _context.Entry(existStudyAssignment).CurrentValues.SetValues(studyAssignment);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var studyAssignment = await _context.StudyAssignments.FindAsync(id);
            _context.Remove(studyAssignment);
            await _context.SaveChangesAsync();
        }

        public bool EntryExists(Guid id)
        {
            return _context.StudyAssignments.Any(e => e.Id == id);
        }
    }
}