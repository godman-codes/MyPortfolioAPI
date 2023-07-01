﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Response
{
    public record TechnologyResponseDto: TechnologyDto
    {
        public Guid Id { get; init; }
    }
}
