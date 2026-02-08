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

        public async Task<bool> ValidateUserAsync(ValidateUserRequest request, CancellationToken ct)
        {
            var isValid = await _userRepository.ValidateUserAsync(request, ct);

            return isValid;
        }

        public async Task<User?> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
        {
            var result = await _userRepository.InsertUserAsync(request, ct);

            return result;
        }
    }
}
