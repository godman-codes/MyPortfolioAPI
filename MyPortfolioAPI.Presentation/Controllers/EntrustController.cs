using EntrustContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolioAPI.Presentation.Controllers
{
    [Route("api/entrust")]
    [ApiController]
    public class EntrustController : ControllerBase
    {
        private readonly IEntrustManager _entrust;

        public EntrustController(IEntrustManager entrust)
        {
            _entrust = entrust;
        }


        [HttpPost("challlenge")]
        public async Task<IActionResult> MakeChallenge(string userId)
        {
            await _entrust.EntrustAuthService.MakeGenericChallengeAsync(userId);
            return Ok();
        }


        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateChallenge(string userId, string otp)
        {
            var response = await _entrust.EntrustAuthService.DoAuthenticateGenericChallengeAync(userId, otp);
            return Ok(response);
        }

        //[HttpPost("genericChallenge")]
        //public async Task<IActionResult> CallGenericChallenge(string userId)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        // Construct the SOAP request body
        //        string soapRequestBody = $@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:urn=""urn:entrust.com:ig:authenticationV11:wsdl"">
        //        <soap:Header/>
        //        <soap:Body>
        //            <urn:getGenericChallengeCallParms>
        //                <userId>{userId}</userId>
        //                <parms>
        //                    <SecurityLevel>NORMAL</SecurityLevel>
        //                    <AuthenticationType>TOKENRO</AuthenticationType>
        //                </parms>
        //            </urn:getGenericChallengeCallParms>
        //        </soap:Body>
        //    </soap:Envelope>";

        //        // Create the HTTP content with the SOAP request
        //        StringContent content = new StringContent(soapRequestBody, System.Text.Encoding.UTF8, "application/soap+xml");

        //        // Set the endpoint URL
        //        string endpointUrl = "http://192.168.130.80:8080/IdentityGuardAuthService/services/AuthenticationServiceV11";

        //        // Make the HTTP POST request
        //        HttpResponseMessage response = await client.PostAsync(endpointUrl, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string soapResponse = await response.Content.ReadAsStringAsync();
        //            // Process the SOAP response as needed
        //            return Ok(soapResponse);
        //        }
        //        else
        //        {
        //            // Handle the error response
        //            throw new Exception("SOAP request failed with status code: " + response.StatusCode);
        //        }
        //    }
        //}
        //[HttpPost("AuthenticateGenericChallenge")]
        //public async Task<string> CallAuthenticationService(string userId, string token)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        // Construct the complete SOAP request body with the userId variable
        //        string soapRequestBody = $@"
        //        <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
        //            <soap:Body>
        //                <authenticateGenericChallengeCallParms xmlns=""urn:entrust.com:ig:authenticationV11:wsdl"">
        //                    <userId>{userId}</userId>
        //                    <response xmlns="""">
        //                        <PVN xsi:nil=""true"" />
        //                        <response>
        //                            <item>{token}</item>
        //                        </response>
        //                        <radiusResponse xsi:nil=""true"" />
        //                    </response>
        //                    <parms xmlns="""">
        //                        <SecurityLevel xsi:nil=""true"" />
        //                        <AuthenticationType>TOKENRO</AuthenticationType>
        //                        <ApplicationName xsi:nil=""true"" />
        //                        <ChallengeSize xsi:nil=""true"" />
        //                        <numWrongAnswersAllowed xsi:nil=""true"" />
        //                        <AuthTypesRequiringPVN xsi:nil=""true"" />
        //                        <serialNumber xsi:nil=""true"" />
        //                        <tokenVendorId xsi:nil=""true"" />
        //                        <tokenSets xsi:nil=""true"" />
        //                        <dataSignatureValues xsi:nil=""true"" />
        //                        <authSecretParms xsi:nil=""true"" />
        //                        <sharedSecretParms xsi:nil=""true"" />
        //                        <registerMachineSecret xsi:nil=""true"" />
        //                        <machineSecret xsi:nil=""true"" />
        //                        <IPAddress xsi:nil=""true"" />
        //                        <certificate xsi:nil=""true"" />
        //                        <externalRiskScore xsi:nil=""true"" />
        //                        <newPassword xsi:nil=""true"" />
        //                        <passwordName xsi:nil=""true"" />
        //                        <newPVN xsi:nil=""true"" />
        //                        <ChallengeHistory xsi:nil=""true"" />
        //                        <transactionId xsi:nil=""true"" />
        //                        <cancelTransaction xsi:nil=""true"" />
        //                        <ReturnCertificateResponse xsi:nil=""true"" />
        //                        <transactionDetails xsi:nil=""true"" />
        //                        <useDefaultDelivery xsi:nil=""true"" />
        //                        <contactInfoLabel xsi:nil=""true"" />
        //                        <deliverForDynamicRefresh xsi:nil=""true"" />
        //                        <performDeliveryAndSignature xsi:nil=""true"" />
        //                        <requireDeliveryAndSignatureIfAvailable xsi:nil=""true"" />
        //                        <retrieveRepositoryAttributes xsi:nil=""true"" />
        //                    </parms>
        //                </authenticateGenericChallengeCallParms>
        //            </soap:Body>
        //        </soap:Envelope>";

        //        // Create the HTTP content with the SOAP request
        //        StringContent content = new StringContent(soapRequestBody, System.Text.Encoding.UTF8, "text/xml");

        //        // Set the endpoint URL
        //        string endpointUrl = "http://192.168.130.80:8080/IdentityGuardAuthService/services/AuthenticationServiceV11";

        //        // Make the HTTP POST request
        //        HttpResponseMessage response = await client.PostAsync(endpointUrl, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string soapResponse = await response.Content.ReadAsStringAsync();
        //            // Process the SOAP response as needed
        //            return soapResponse;
        //        }
        //        else
        //        {
        //            // Handle the error response
        //            throw new Exception("SOAP request failed with status code: " + response.StatusCode);
        //        }
        //    }
        //}
    }
}
