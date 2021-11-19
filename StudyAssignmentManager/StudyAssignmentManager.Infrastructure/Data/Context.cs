using System;
using Microsoft.EntityFrameworkCore;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {}
        
        public DbSet<StudyAssignment> StudyAssignments { get; set; }
        public DbSet<AssignmentData> AssignmentDatas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<CheckRequest> CheckRequests { get; set; }
    }
}