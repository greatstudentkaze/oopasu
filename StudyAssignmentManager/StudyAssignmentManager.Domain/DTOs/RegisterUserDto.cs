using System;

namespace StudyAssignmentManager.Domain
{
    public class RegisterUserDto
    {
        public String Email { get; set; }
        public String FullName { get; set; }
        public String Password { get; set; }
    }
}