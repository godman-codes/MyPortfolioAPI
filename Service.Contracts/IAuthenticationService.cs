using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.DTOs.Request;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<string> GetEmailConfirmationToken(UserModel user);
    }
}
