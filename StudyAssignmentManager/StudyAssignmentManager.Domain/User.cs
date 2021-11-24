using System;
using System.Collections.Generic;

namespace StudyAssignmentManager.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }
        public String PasswordHash { get; set; }
        
        public List<Guid> RoleIds { get; set; }
        public List<UserRole> Roles { get; set; }
        public List<CheckRequest> CheckRequests { get; set; }
    }
}