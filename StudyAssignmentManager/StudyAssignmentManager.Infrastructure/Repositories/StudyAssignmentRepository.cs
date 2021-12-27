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
            return await _context.StudyAssignments
                .Where(it => it.Id == id)
                .Include(it => it.EducationalMaterial)
                .Include(it => it.Teacher)
                .Include(it => it.Tutor)
                .Include(it => it.CheckRequests)
                    .ThenInclude(it => it.Reviewer)
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByTutorIdAsync(Guid id)
        {
            return await _context.StudyAssignments
                .Where(it => it.TutorId == id)
                .Include(it => it.Tutor)
                .Include(it => it.Student)
                .ToListAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByTeacherIdAsync(Guid id)
        {
            return await _context.StudyAssignments
            .Where(it => it.TeacherId == id || it.TutorId == id)
            .Include(it => it.Tutor)
            .Include(it => it.Student)
            .Include(it => it.EducationalMaterial)
            .ToListAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByStudentIdAsync(Guid id)
        {
            return await _context.StudyAssignments.Where(it => it.StudentId == id).ToListAsync();
        }
        
        public async Task<List<StudyAssignment>> GetByEduMaterialIdAsync(Guid id)
        {
            return await _context.StudyAssignments.Where(it => it.EducationalMaterialId == id).ToListAsync();
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