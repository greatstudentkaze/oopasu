using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        
        public AttachmentRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<Attachment> GetByIdAsync(Guid id)
        {
            return await _context.Attachments.FindAsync(id);
        }
        
        public async Task AddAsync(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Attachment attachment)
        {
            _context.Remove(attachment);
            await _context.SaveChangesAsync();
        }
    }
}