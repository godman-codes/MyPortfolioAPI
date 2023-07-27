using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs.Request;
using System.Security.Claims;

namespace MyPortfolioAPI.Presentation.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProjectController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetProjects()
        {
            string userId = GetUser();
            var projects = await _service.ProjectService.GetAllProjects(userId, false);
            return Ok(projects);
        }

        [HttpGet("{Id:Guid}", Name = "GetProjectById")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProject(Guid Id)
        {
            string userId = GetUser();
            var project = await _service.ProjectService.GetProject(Id, userId, false);
            return Ok(project);
        }

        [HttpPost(Name = "CreatedProject")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestDto project)
        {
            string user = GetUser();
            var CreatedProject = await _service.ProjectService.CreateProject(project, user, false);
            return CreatedAtRoute("GetProjectById", new { Id = CreatedProject.Id }, CreatedProject);
        }

        [HttpPut("{Id:Guid}")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectToUpdateDto projectToUpdate, Guid Id)
        {
            string user = GetUser();
            await _service.ProjectService.UpdateProject(Id, projectToUpdate, user, true);
            return NoContent();
        }

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Developer")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProject(Guid Id)
        {
            string user = GetUser();
            await _service.ProjectService.DeleteProject(Id, user, true);
            return NoContent();
        }


        private protected string GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
