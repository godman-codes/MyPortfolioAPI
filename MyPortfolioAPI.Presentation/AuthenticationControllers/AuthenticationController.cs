
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs.Request;
using Shared.DTOs.Response;
using Utilities.Constants;

namespace MyPortfolioAPI.Presentation.AuthenticationController
{
    [Route("api/authentication")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            NewUserDto _ = new() { message = Constants.NewAccountMessage };
            return Ok(_);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status401Unauthorized)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var userResponse = await _service.AuthenticationService.ValidateUser(user);

            return Ok(userResponse);
        }

        //public async Task<IActionResult> AuthenticateMFA([FromBody] LoggedInUserResponseDto loggedInUserResponse, string token)
        //{
        //    var authenticated = await _
        //}




        [HttpPost("ActivateAccount")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActivateAccount([FromBody] AccountActivationByEmailDto accountActivation)
        {
            var result = await _service.AuthenticationService.ActivateAccount(accountActivation);
            if (!result.Succeeded)
            {
                return BadRequest(Constants.AccountActivationErrorMessage);
            }
            return Ok(Constants.AccountActivationMessage);

        }

    }
}
