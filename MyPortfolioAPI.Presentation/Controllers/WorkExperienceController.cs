using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs.Request;
using System.Security.Claims;


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

        [HttpGet]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllWorkExperiences()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workExperiences = await _service.WorkExperienceService.GetAllWorkExperience(trackChanges: false, userId);

            return Ok(workExperiences);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, [FromBody] WorkExperienceToUpdateDto workExperienceToUpdate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.WorkExperienceService.UpdateWorkExperience(id, workExperienceToUpdate, trackChanges: true, userId);
            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.WorkExperienceService.DeleteWorkExperience(id, trackChanges:true, userId);
            return NoContent();
        }

    }
}
