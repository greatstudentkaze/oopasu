using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class StudentsContext : DbContext
    {
        public StudentsContext(DbContextOptions<StudentsContext> options) : base(options)
        {
        }

        public DbSet<StudentsGroup> StudentsGroups { get; set; }
    }
}