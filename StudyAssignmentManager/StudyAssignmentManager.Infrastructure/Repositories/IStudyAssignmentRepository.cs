using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public interface IStudyAssignmentRepository
    {
        Task<StudyAssignment> GetByIdAsync(Guid id);
        Task<List<StudyAssignment>> GetByTutorIdAsync(Guid id);
        Task<List<StudyAssignment>> GetByTeacherIdAsync(Guid id);
        Task<List<StudyAssignment>> GetByStudentIdAsync(Guid id);
        Task<List<StudyAssignment>> GetByEduMaterialIdAsync(Guid id);
        Task AddAsync(StudyAssignment studyAssignment);
        Task UpdateAsync(StudyAssignment studyAssignment);
        Task DeleteAsync(Guid id);
        bool EntryExists(Guid id);
    }
}