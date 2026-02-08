using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Domain.Users
{
    public class User
    {
        public required int Id { get; init; }
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required DateTime CreatedDate { get; init; }
        public required bool IsActive { get; init; }
        public required string Role { get; init; }
    }
}
