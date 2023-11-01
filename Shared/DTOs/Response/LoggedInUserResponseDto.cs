using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Response
{
    public record LoggedInUserResponseDto
    {
        public string UserId { get; init; }
        public bool Authenticated { get; init; }

        public bool EntrustUser { get; init; }
        public bool ActivatedUser { get; init; }
    }
}
