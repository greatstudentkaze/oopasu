using System;
using System.Collections.Generic;
using StudyAssignmentManager.Domain.Enums;

namespace StudyAssignmentManager.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        
        public List<CheckRequest> CheckRequests { get; set; }
        public List<AssignmentData> AssignmentDataList { get; set; }
    }
}