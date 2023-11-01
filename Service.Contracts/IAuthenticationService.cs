using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.DTOs.Request;
using Shared.DTOs.Response;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<LoggedInUserResponseDto> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<string> GetEmailConfirmationToken(UserModel user);
        Task<IdentityResult> ActivateAccount(AccountActivationByEmailDto accountActivation);
        Task<bool> ValidateMFA(LoggedInUserResponseDto loggedInUserResponse, string token);
    }
}
