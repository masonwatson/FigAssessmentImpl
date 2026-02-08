using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Users.CreateUsers
{
    public class CreateUserRequest
    {
        public string Username { get; init; } = "";
        public string Email { get; init; } = "";
        public string Password { get; init; } = "";
        public bool IsActive { get; init; } = false;
        public string Role { get; init; } = "";
    }
}
