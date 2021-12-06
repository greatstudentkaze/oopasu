using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public interface IAttachmentRepository
    {
        Task<Attachment> GetByIdAsync(Guid id);
        Task AddAsync(Attachment answer);
        Task DeleteAsync(Attachment answer);
    }
}