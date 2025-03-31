using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace AzureFunctions.Services;

public class ValidateTokenService
{
    private readonly string _instance;
    private readonly string _tenant;
    private readonly string _policy;
    private readonly string _audience;
    private readonly string _issuer;
    private readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

    public ValidateTokenService()
    {
        _instance = Environment.GetEnvironmentVariable("AzureAdB2C__Instance")!;
        _tenant = Environment.GetEnvironmentVariable("AzureAdB2C__Tenant")!;
        _policy = Environment.GetEnvironmentVariable("AzureAdB2C__Policy")!;
        _audience = Environment.GetEnvironmentVariable("AzureAdB2C__ClientId")!;

        _issuer = $"{_instance}/{_tenant}/v2.0/";

        var metadataAddress = $"{_instance}/{_tenant}/v2.0/.well-known/openid-configuration?p={_policy}";
        _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            metadataAddress,
            new OpenIdConnectConfigurationRetriever()
        );
    }

    public async Task<ClaimsPrincipal> ValidateTokenAsync(string token)
    {
        var openIdConfig = await _configurationManager.GetConfigurationAsync(CancellationToken.None);

        var validationParameters = new TokenValidationParameters
        {
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = openIdConfig.SigningKeys,
            ClockSkew = TimeSpan.Zero
        };

        var handler = new JwtSecurityTokenHandler();
        return handler.ValidateToken(token, validationParameters, out _);
    }
}