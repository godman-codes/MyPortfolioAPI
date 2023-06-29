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
    [ApiController]
    [Route("api/workexperience")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkExperienceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public WorkExperienceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost(Name = "CreateWorkExperience")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> CreateWorkExperience([FromBody] WorkExperienceRequestDto workExperienceRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var createdWorkExperience = await _service.WorkExperienceService.CreateWorkExperience(workExperienceRequest, userId);

            return CreatedAtRoute("WorkExperienceById", new { id = createdWorkExperience.Id }, createdWorkExperience );
        }


        [HttpGet("{id:guid}", Name = "WorkExperienceById")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetWorkExperience(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var WorkExperience = await _service.WorkExperienceService.GetWorkExperience(id, trackchanges: false, userId);
            return Ok(WorkExperience);
        }
    }
}
