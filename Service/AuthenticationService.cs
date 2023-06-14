
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DTOs;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<UserModel> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(
            ILoggerManager logger,
            IMapper mapper,
            UserManager<UserModel> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager
            )
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _roleManager = roleManager;

            
        }
        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<UserModel>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (result.Succeeded)
            {
                // filter theroles that exist in the role manager
                var validRoles = userForRegistration.Roles
                  .Where(role => _roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                  .ToList();

                // Add user to the roles and user join table
                await _userManager.AddToRolesAsync(user, validRoles);
            }
            return result;
        }
    }
}
