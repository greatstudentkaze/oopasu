using System;
using System.Collections.Generic;

namespace StudyAssignmentManager.Domain
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        
        public List<User> Users { get; set; }
    }
}