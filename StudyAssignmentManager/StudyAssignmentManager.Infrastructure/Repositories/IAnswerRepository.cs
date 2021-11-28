using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> GetByIdAsync(Guid id);
        Task<List<Answer>> GetByAssignmentIdAsync(Guid id);
        Task AddAsync(Answer answer);
        Task UpdateAsync(Answer answer);
        Task AddCommentAsync(Guid id, AddCommentDto model);
        Task DeleteAsync(Answer answer);
        bool EntryExists(Guid id);
    }
}