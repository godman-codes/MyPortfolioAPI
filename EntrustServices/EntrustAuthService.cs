using Contracts;
using Entities.Exceptions;
using EntrustContracts;
using EntrustIdentityGuard;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using Utilities.Constants;

namespace EntrustServices
{
    public class EntrustAuthService : IEntrustAuthService
    {
        //private readonly string _urlString = "https://192.168.130.80:8443/IdentityGuardAuthService/services/AuthenticationServiceV11";
        private readonly ILoggerManager _logger;
        private readonly AuthenticationService _authenticationService;


        //public EntrustAuthService(ILoggerManager logger)
        //{
        //    _logger = logger;
        //    _authBinding = new AuthenticationService()
        //    {
        //        Url = _urlString
        //    };
        //}

        //public async Task DoGetGenericChallengeAsync(string userId)
        //{
        //    GetGenericChallengeCallParms getGenericChallengeCallParms = new GetGenericChallengeCallParms()
        //    {
        //        userId = userId,
        //        parms = new GenericChallengeParms() 
        //        {
        //            AuthenticationType = AuthenticationType.TOKENRO,
        //            SecurityLevel = SecurityLevel.NORMAL 
        //        }
        //    };

        //    try
        //    {

        //        _authBinding.getGenericChallengeAsync(getGenericChallengeCallParms);
        //    }
        //    catch (SoapException soapEx)
        //    {
        //        AuthenticationFault fault = AuthenticationService.getFault(soapEx);
        //        if (fault != null)
        //        {
        //            // Handle the IdentityGuard fault
        //            _logger.LogError(fault.errorMessage);
        //            System.Console.WriteLine(fault.errorMessage);
        //        }
        //        else
        //        {
        //            _logger.LogError(soapEx.Message);
        //            // Not an IG fault, handle the generic SoapException
        //            System.Console.WriteLine(soapEx.Message);
        //        }
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        // Handle non-SOAP exception, e.g., connection error
        //        System.Console.WriteLine(ex.Message);
        //        return;
        //    }

        //    return;
        //}


        //public async Task<bool> DoAuthenticateGenericChallengeAync(string userId, string otp)
        //{
        //    Response response = new Response();

        //    GenericAuthenticateParms authParms = new GenericAuthenticateParms();

        //    //TokenChallenge tokenChallenge = genericChallenge.TokenChallenge;

        //    // Get the token challenge
        //    // For simplicity, we assume the user only has one CR token
        //    String[] userResponse = { otp };
        //    response.response = userResponse;

        //    // Once the client application gets a challenge response from the
        //    // user, authenticate the response

        //    try
        //    {
        //        authParms.AuthenticationType = AuthenticationType.TOKENRO;
        //        AuthenticateGenericChallengeCallParms authCallParms = new AuthenticateGenericChallengeCallParms()
        //        {
        //            userId = userId,
        //            parms = authParms,
        //            response = response
        //        };
        //        GenericAuthenticateResponse authResponse = _authBinding.authenticateGenericChallenge(authCallParms);

        //        string user = authResponse.FullName;

        //        if (user == null)
        //            user = userId;

        //        return true;
        //    }
        //    catch (SoapException soapEx)
        //    {
        //        AuthenticationFault fault = AuthenticationService.getFault(soapEx);
        //        if (fault != null)
        //        {
        //            // Handle the IdentityGuard fault
        //            System.Console.WriteLine(fault.errorMessage);
        //            _logger.LogError(fault.errorMessage);
        //            return false;
        //        }
        //        else
        //        {
        //            // Not an IG fault, handle the generic SoapException
        //            System.Console.WriteLine(soapEx.Message);
        //            _logger.LogError(soapEx.Message);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle non-soap exception, e.g., connection error
        //        System.Console.WriteLine(ex.Message);
        //        _logger.LogError(ex.Message);
        //        return false;
        //    }
        //}

        public EntrustAuthService(ILoggerManager logger)
        {
            _logger = logger;
            _authenticationService = new AuthenticationServiceClient();
            


        }

        public async Task<bool> DoAuthenticateGenericChallengeAync(string userId, string otp)
        {
            AuthenticateGenericChallengeCallParms authCallParms = new AuthenticateGenericChallengeCallParms();
            GenericAuthenticateParms authParams = new GenericAuthenticateParms();
            Response _response = new();
            String[] _otp = { otp };
            _response.response = _otp;
            authCallParms.userId = userId;
            authCallParms.response = _response;
            authParams.AuthenticationType = AuthenticationType.TOKENRO;

            authCallParms.parms = authParams;

            authenticateGenericChallengeRequest authRequest = new()
            {
                authenticateGenericChallengeCallParms = authCallParms,
            };
            try
            {

                authenticateGenericChallengeResponse authResponse = await _authenticationService.authenticateGenericChallengeAsync(authRequest);

                GenericAuthenticateResponse authReturn = authResponse.authenticateGenericChallengeReturn;

                return true;
            }

            catch (FaultException<AuthenticationFaultInfo> ex)
            {
                switch(ex.Detail.ErrorCode)
                {
                    case ErrorCode.INVALID_RESPONSE:
                        _logger.LogError(ex.Detail.errorMessage);
                        return false;
                    case ErrorCode.USER_SUSPENDED:
                        _logger.LogError(ex.Detail.errorMessage);
                        throw new EntrustException(Constants.EntrustBlockedUser);
                    case ErrorCode.USER_DOES_NOT_EXIST:
                        _logger.LogError(ex.Detail.errorMessage);
                        throw new EntrustException(Constants.UserNotActivated);
                    default:
                        _logger.LogError(ex.Detail.errorMessage);
                        throw new EntrustException(Constants.EntrustGeneralError);
                } 
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new EntrustException(Constants.EntrustConnectionError);
            }
                

        }

        public async Task<bool> MakeGenericChallengeAsync(string userId)
        {
            GenericChallengeParms genericChallengeParms = new GenericChallengeParms()
            {
                AuthenticationType = AuthenticationType.TOKENRO,
                SecurityLevel = SecurityLevel.NORMAL
            };

            GetGenericChallengeCallParms getGenericChallengeCallParms = new()
            {
                userId = userId,
                parms = genericChallengeParms
            };

            getGenericChallengeRequest _getGenericChallengeRequest = new()
            {
                getGenericChallengeCallParms = getGenericChallengeCallParms
            };
            
            try
            {

                getGenericChallengeResponse _getGenericChallengeResponse = await _authenticationService.getGenericChallengeAsync(_getGenericChallengeRequest);

                GenericChallenge challenge = _getGenericChallengeResponse.getGenericChallengeReturn;

                return true;
            }
            catch (FaultException<AuthenticationFaultInfo> ex)
            {
                 if (ex.Detail.ErrorCode == ErrorCode.USER_DOES_NOT_EXIST)
                {
                    
                    _logger.LogError(ex.Detail.errorMessage);
                    return false;

                }
                else
                {
                    _logger.LogError(ex.Detail.errorMessage);
                    throw new EntrustException(Constants.EntrustGeneralError);
                    
                }

            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new EntrustException(Constants.EntrustConnectionError);
            }

            
        }       

    }
}
