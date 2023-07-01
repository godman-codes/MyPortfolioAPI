using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolioAPI.Presentation.Controllers
{
    [Route("api/technology")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TechnologyController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TechnologyController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> GetTechnologies()
        {
            var userId = GetUser();

            var technologies = await _service.TechnologyService.GetAllTechnologies(userId, false);
            return Ok(technologies);
        }

        private protected string GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        [HttpGet("{id:Guid}", Name = "GetTechnologyById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> GetTechnology(Guid id)
        {
            var userId = GetUser();
            var technlology = await _service.TechnologyService.GetTechnology(id, false, userId);
            return Ok(technlology);
        }

        [HttpPost(Name = "CreateTechnology")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        [Authorize(Roles = "Developer")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTechnology(TechnologyRequestDto technologyRequest)
        {
            var userId = GetUser();
            var technologyResponse = await _service.TechnologyService.CreateTechnology(technologyRequest, userId);
            return CreatedAtRoute("GetTechnologyById", new { id = technologyResponse.Id }, technologyResponse);
        }


        [HttpPut("{id:Guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        [Authorize(Roles = "Developer")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<IActionResult> UpdateTechnology(Guid id, [FromBody] TechnologyUpdateDto technologyUpdate)
        {
            var userId = GetUser();
            await _service.TechnologyService.UpdateTechnology(id, technologyUpdate, userId, trackChanges: true);
            return NoContent();

        }


        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(204)]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> DeleteTechnology(Guid id)
        {
            var userId = GetUser();
            await _service.TechnologyService.DeleteTechnology(id, userId, true);
            return NoContent();
        }
    }
}
