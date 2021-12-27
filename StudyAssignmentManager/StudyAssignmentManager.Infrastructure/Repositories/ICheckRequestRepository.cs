using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Domain.Enums;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public interface ICheckRequestRepository
    {
        Task<List<CheckRequest>> GetAllAsync();
        Task<CheckRequest> GetByIdAsync(Guid id);
        Task<CheckRequest> GetByIdWithAssignmentAsync(Guid id);
        Task<List<CheckRequest>> GetByAssignmentIdAsync(Guid id);
        Task<List<CheckRequest>> GetByReviewerIdAsync(Guid id);
        Task AddAsync(CheckRequest checkRequest);
        Task UpdateAsync(CheckRequest checkRequest);
        Task UpdateStatusAsync(Guid id, CheckRequestStatus status);
        public int GetCount();
    }
}