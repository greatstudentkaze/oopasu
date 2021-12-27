using System;

namespace StudyAssignmentManager.Domain
{
    public class AuthenticationResponse : UserData
    {
        // public string Token { get; set; }

        public AuthenticationResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FullName = user.FullName;
            Role = user.Role;
        }
    }
}