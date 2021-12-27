using System;
using StudyAssignmentManager.Domain.Enums;

namespace StudyAssignmentManager.Domain
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
    }
}