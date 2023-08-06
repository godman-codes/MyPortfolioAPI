
using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Exceptions;
using Entities.Models;
using Entities.SystemModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DTOs;
using Shared.DTOs.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Utilities.Constants;
using Utilities.Enum;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<UserModel> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        private UserModel? _user;



        public AuthenticationService(
            ILoggerManager logger,
            IMapper mapper,
            UserManager<UserModel> userManager,
            IOptions<JwtConfiguration> configuration,
            RoleManager<IdentityRole> roleManager,
            IRepositoryManager repository
            )
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtConfiguration = _configuration.Value;
            _repository = repository;

            
        }


        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<UserModel>(userForRegistration);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _repository.EmailRepository.CreateEmailLog(
                new EmailModel()
                {
                    Id = Guid.NewGuid(),
                    Emailaddresses = user.Email,
                    EmailType = EmailTypeEnums.AccountActivation,
                    Status = MessageStatusEnums.Pending,
                    UserId = user.Id,
                    NewUserActivationToken = code
                });

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

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed invalid credentails");
            return result;
        }
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            // Get the signing credentials used to sign the token
            var signingCredentials = GetSigningCredentials();

            // Get the claims associated with the authenticated user
            var claims = await GetClaims();

            // Generate the options for the JWT token
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            // generate RefreshToken 
            var refreshToken = GenerateRefreshToken();

            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            // Write the token as a string
            return new TokenDto(accessToken, refreshToken);
        }


        private static SigningCredentials GetSigningCredentials()
        {
            // Get the secret key from the environment variable
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(Constants.Secret));

            // Create a new symmetric security key using the secret key
            var secret = new SymmetricSecurityKey(key);

            // Return the signing credentials using the symmetric security key
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                // Add the user's name as a claim
                new Claim(ClaimTypes.Name, value: _user.Email),
                new Claim(type: "Id", value: _user.Id),
                new Claim(type: JwtRegisteredClaimNames.Sub, value: _user.Id),
                new Claim(type: JwtRegisteredClaimNames.Email, value: _user.Email),
                new Claim(type: JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())

            };

            // Get the roles associated with the user
            var roles = await _userManager.GetRolesAsync(_user);

            // Add each role as a claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        // Generate options for JWT token
        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials,
            List<Claim> claims)
        {
            // Get the JWT settings from the configuration

            // Create a new JWT security token with the specified options
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _userManager.FindByEmailAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();

            _user = user;

            return await CreateToken(populateExp: false);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            // Set up token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(Constants.Secret))),
                ValidateLifetime = false,
                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience
            };

            // Create a new JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Validate the token and retrieve the principal
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            // Check if the token is a valid JwtSecurityToken
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                // Throw an exception if the token is invalid
                throw new SecurityTokenException(string.Format(Constants.InvalidSubject, Constants.Token));
            }

            // Return the principal extracted from the token
            return principal;
        }

        public async Task<string> GetEmailConfirmationToken(UserModel user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }
    }
}
