using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure.Repositories
{
    public class EducationalMaterialRepository
    {
        private readonly Context _context;
        
        public EducationalMaterialRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<EducationalMaterial> GetByIdAsync(Guid id)
        {
            return await _context.EducationalMaterials.FindAsync(id);
        }
        
        public async Task<List<EducationalMaterial>> GetByAuthorIdAsync(Guid id)
        {
            return await _context.EducationalMaterials.Where(it => it.AuthorId == id).ToListAsync();
        }
        
        public async Task AddAsync(EducationalMaterial educationalMaterial)
        {
            _context.EducationalMaterials.Add(educationalMaterial);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(EducationalMaterial educationalMaterial)
        {
            var existEntry = await _context.EducationalMaterials.FindAsync(educationalMaterial.Id);
            _context.Entry(existEntry).CurrentValues.SetValues(educationalMaterial);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var educationalMaterial = await _context.EducationalMaterials.FindAsync(id);
            _context.Remove(educationalMaterial);
            await _context.SaveChangesAsync();
        }

        public bool EntryExists(Guid id)
        {
            return _context.EducationalMaterials.Any(e => e.Id == id);
        }
    }
}