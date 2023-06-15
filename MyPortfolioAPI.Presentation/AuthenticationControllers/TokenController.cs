using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolioAPI.Presentation.AuthenticationControllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TokenController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var returnToken = await _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(returnToken);
        }


    }
}
