using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using wormhole.policy.aspnetcore;

namespace wormhole.policy
{
    //public class OpenIdConnectJwtPolicyConfig
    //{
    //    public string MetadataEndpoint { get; set; }
    //    public string ValidIssuer { get; set; }
    //    public List<string> ValidAudiences { get; set; }
    //    //We will validate other properties in future
    //}

    //OpenIdConnectJwtPolicy
    public class OpenIdConnectJwtPolicy : IPolicy
    {
        public ServiceTypeEnum ServiceType { get; set; }
        //readonly OpenIdConnectJwtPolicyConfig _config = null;
        readonly JwtSecurityTokenHandler _handler = null;
        readonly TokenValidationParameters _validationParameters = null;
        public OpenIdConnectJwtPolicy(Dictionary<string, string> config)
        {
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                //"https://login.microsoftonline.com/PSGCustomerDev.onmicrosoft.com/v2.0/.well-known/openid-configuration?p=B2C_1_absg-custom-login-ui",
                config["MetadataEndpoint"],
                new OpenIdConnectConfigurationRetriever());

            OpenIdConnectConfiguration openIdConfig = configurationManager.GetConfigurationAsync(CancellationToken.None).Result;

            _validationParameters = new TokenValidationParameters
            {
                ValidIssuer = config["ValidIssuer"],
                //ValidAudiences = ValidAudiencesList, //new[] { audience },
                IssuerSigningKeys = openIdConfig.SigningKeys
            };

            if (config.ContainsKey("ValidAudiences"))
            {
                var ValidAudiences = config["ValidAudiences"].Split(';');
                var ValidAudiencesList = ValidAudiences.Where(x => !string.IsNullOrWhiteSpace(x));
                _validationParameters.ValidAudiences = ValidAudiencesList;
            }


            _handler = new JwtSecurityTokenHandler();
        }

        public bool Run(HttpContext context, ProxyOptions options)
        {
            try
            {
                //Authorization
                var tokenWithBearer = context.Request.Headers["Authorization"].First();
                var token = tokenWithBearer.Substring("Bearer ".Length);
                SecurityToken validatedToken;
                var user = _handler.ValidateToken(token, _validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return false;
        }
    }
}


/*
    <PackageReference Include="Microsoft.IdentityModel.Protocols.OpenIdConnect" Version="5.2.0-preview1-408290725" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="5.2.0-preview1-408290725" />

using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;

namespace ConsoleAppValidateJwt
{
    class Program
    {
        //http://www.jerriepelser.com/blog/manually-validating-rs256-jwt-dotnet/
        //https://github.com/auth0-samples/auth0-dotnet-validate-jwt/blob/master/IdentityModel-RS256/Program.cs

        const string issuer = "https://login.microsoftonline.com/aede7b95-4764-4d15-8739-02c90198649e/v2.0/"; // Your Auth0 domain
        const string audience = "36e2d53f-b909-4977-aece-1ed1a49c9722"; // Your API Identifier

        static void Main(string[] args)
        {
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vYWVkZTdiOTUtNDc2NC00ZDE1LTg3MzktMDJjOTAxOTg2NDllL3YyLjAvIiwiZXhwIjoxNTA5MzIxNTAzLCJuYmYiOjE1MDkyMzUxMDMsImF1ZCI6IjM2ZTJkNTNmLWI5MDktNDk3Ny1hZWNlLTFlZDFhNDljOTcyMiIsIm9pZCI6IjI5YWU0ODNlLWQyMDUtNGY4Mi1hOGI4LWEyZmNjODQ5Yzc4NyIsInN1YiI6IjI5YWU0ODNlLWQyMDUtNGY4Mi1hOGI4LWEyZmNjODQ5Yzc4NyIsImV4dGVuc2lvbl91c2VycGFydHlpZCI6IjMiLCJuYW1lIjoiVGVzdCBQcmFjdGljZUFkbWluIiwidGZwIjoiQjJDXzFfYWJzZy1jdXN0b20tbG9naW4tdWkiLCJub25jZSI6IjJkMWFjMzg2MjJlOTQxYTdiZDVkZmJiNjRkMzk5NmYzIiwiYXpwIjoiMzZlMmQ1M2YtYjkwOS00OTc3LWFlY2UtMWVkMWE0OWM5NzIyIiwidmVyIjoiMS4wIn0.n0LDPC3JeVm4XFS8Oe8-LAl7y4-C3vifQIjrpscEu6cVn9OR1QmQlVRjWyU8irwVSBFxzZ7aQwUrDmq1wEIHE4pf2t_wvKBzkMb7sog5QbcvKF45sAS64vOX0Yb3D70fG3lQvEvt_4jLq2orvwhQkJUv4ldwbIBbTKd_1vggNRCnBbwaNwwsOhgsHP8tKlvWEWbzPiXt9sP5RXSDQe7VF7ukL3q6VDYguUsRhUbOiE8RunjBqcQp9V1971r8IguZNCdf07Pgq-Tw9I2ODwG5PTl8uEJjn_GpjwxMV4ThrpwRn6pbetVwSOTFKtlhxdG67I34TlZEioPixyVX-E1VFA";

            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                "https://login.microsoftonline.com/PSGCustomerDev.onmicrosoft.com/v2.0/.well-known/openid-configuration?p=B2C_1_absg-custom-login-ui",
                new OpenIdConnectConfigurationRetriever());

            OpenIdConnectConfiguration openIdConfig = configurationManager.GetConfigurationAsync(CancellationToken.None).Result;

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudiences = new[] { audience },
                IssuerSigningKeys = openIdConfig.SigningKeys
            };

            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var user = handler.ValidateToken(token, validationParameters, out validatedToken);

            Console.WriteLine("Done");
        }
    }
}
 
     */
