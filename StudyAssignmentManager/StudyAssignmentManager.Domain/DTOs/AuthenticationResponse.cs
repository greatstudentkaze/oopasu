using System;

namespace StudyAssignmentManager.Domain
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        // public string Token { get; set; }

        public AuthenticationResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FullName = user.FullName;
        }
    }
}