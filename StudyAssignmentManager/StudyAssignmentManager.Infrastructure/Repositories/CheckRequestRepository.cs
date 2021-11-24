using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public class CheckRequestRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        
        public CheckRequestRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<List<CheckRequest>> GetAllAsync()
        {
            return await _context.CheckRequests.ToListAsync();
        }
        
        public async Task<CheckRequest> GetByIdAsync(Guid id)
        {
            return await _context.CheckRequests.FindAsync(id);
        }
        
        public async Task<List<CheckRequest>> GetByAssignmentIdAsync(Guid assignmentId)
        {
            return await _context.CheckRequests.Where(it => it.AssignmentId == assignmentId).ToListAsync();
        }
        
        public async Task AddAsync(CheckRequest checkRequest)
        {
            _context.CheckRequests.Add(checkRequest);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(CheckRequest checkRequest)
        {
            var existCheckRequest = await _context.CheckRequests.FindAsync(checkRequest.Id);
            _context.Entry(existCheckRequest).CurrentValues.SetValues(checkRequest);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var checkRequest = await _context.CheckRequests.FindAsync(id);
            _context.Remove(checkRequest);
            await _context.SaveChangesAsync();
        }
    }
}