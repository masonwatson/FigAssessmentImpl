using FigAssessmentImpl.Application.Users.CreateUsers;
using FigAssessmentImpl.Application.Users.GetUsers;
using FigAssessmentImpl.Application.Users.ValidateUsers;
using FigAssessmentImpl.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Users
{
    public interface IUserService
    {
        Task<IReadOnlyList<GetUserResponse>?> GetUsersAsync(GetUserQueryOptions options, CancellationToken ct);
        Task<bool> ValidateUserAsync(ValidateUserRequest request, CancellationToken ct);
        Task<User?> CreateUserAsync(CreateUserRequest request, CancellationToken ct);
    }
}
