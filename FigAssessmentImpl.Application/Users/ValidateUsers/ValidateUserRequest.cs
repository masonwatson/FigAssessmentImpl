using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Users.ValidateUsers
{
    public sealed class ValidateUserRequest
    {
        public string? Username { get; init; }
        public string? Password { get; init; }
    }
}
