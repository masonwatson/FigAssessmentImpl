using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Application.Users.GetUsers
{
    public sealed class GetUserQueryOptions
    {
        public int? Id { get; init; }
        public string? Username { get; init; }
        public string? Email { get; init; }
        public bool? IsActive { get; init; }
        public string? Role { get; init; }
    }
}
