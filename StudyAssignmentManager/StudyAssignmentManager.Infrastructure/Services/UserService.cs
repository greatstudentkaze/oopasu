using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyAssignmentManager.Domain;
using StudyAssignmentManager.Infrastructure.Repositories;

namespace StudyAssignmentManager.API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                return null;
            }

            var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                return null;
            }

            return new AuthenticationResponse(user);
        }

        public async Task<AuthenticationResponse> Register(RegisterUserDto model)
        {
            var candidate = await _userRepository.GetByEmailAsync(model.Email);
            if (candidate != null)
            {
                return null;
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                RoleIds = new List<Guid> {Guid.NewGuid(), Guid.NewGuid()},
            };
            await _userRepository.AddAsync(user);

            var response = await Login(new AuthenticationRequest
            {
                Email = model.Email,
                Password = model.Password,
            });
            return response;
        }
    }
}