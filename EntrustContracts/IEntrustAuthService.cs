using IdentityGuardAuthServiceV12API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrustContracts
{
    public interface IEntrustAuthService
    {
        Task MakeGenericChallengeAsync(string userId);
        Task<bool> DoAuthenticateGenericChallengeAync(string userId, string otp);
    }

}