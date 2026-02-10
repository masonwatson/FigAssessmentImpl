using FigAssessmentImpl.Application.Products.CreateProducts;
using FigAssessmentImpl.Application.Products.GetProducts;
using FigAssessmentImpl.Application.Products;
using FigAssessmentImpl.Application.Users.CreateUsers;
using FigAssessmentImpl.Application.Users.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FigAssessmentImpl.Application.Users.ValidateUsers;
using FigAssessmentImpl.Domain.Users;

namespace FigAssessmentImpl.Application.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyList<GetUserResponse>?> GetUsersAsync(GetUserQueryOptions options, CancellationToken ct)
        {
            var users = await _userRepository.GetUsersAsync(options, ct);

            return users?.Select(u => new GetUserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Password = u.Password,
                CreatedDate = u.CreatedDate,
                IsActive = u.IsActive,
                Role = u.Role,
            }).ToList();
        }

        public async Task<GetUserResponse?> GetUserByIdAsync(int id, CancellationToken ct)
        {
            var user = await _userRepository.GetUserByIdAsync(id, ct);

            return user != null ? new GetUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                IsActive = user.IsActive,
                Role = user.Role,
            } : null;
        }

        public async Task<bool> ValidateUserAsync(ValidateUserRequest request, CancellationToken ct)
        {
            // Validate required fields, min/max lengths, and ranges
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new ArgumentException("You must include a username");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("You must include a password");

            var isValid = await _userRepository.ValidateUserAsync(request, ct);

            return isValid;
        }

        public async Task<GetUserResponse?> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
        {
            // Validate required fields, min/max lengths, and ranges
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new ArgumentException("You must include a username");

            if (request.Username.Length <= 5)
                throw new ArgumentException("Username must be at least 5 characters");

            if (request.Username.Length >= 18)
                throw new ArgumentException("Username must be at most 18 characters");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("You must include an Email");

            // Imagine this is regex for validating that the email sent over is actually in the format of an email

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("You must include a password");

            if (request.Password.Length <= 8)
                throw new ArgumentException("Password must be at least 8 characters");

            // Now imagine this is regex validation for special characters in the password

            var user = await _userRepository.InsertUserAsync(request, ct);

            return user != null ? new GetUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                IsActive = user.IsActive,
                Role = user.Role,
            } : null;
        }
    }
}
