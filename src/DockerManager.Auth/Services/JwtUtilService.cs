using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DockerManager.Auth.Models.Services;
using DockerManager.Auth.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DockerManager.Auth.Services;

internal class JwtUtilService(IConfiguration configuration) : IJwtUtilService
{
    public string GenerateJwtToken(DockerManagerUserWithRoles dockerManagerUserWithRoles)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        if (jwtOptions is null)
        {
            throw new InvalidOperationException("JWT options are not configured in the application settings.");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Email, dockerManagerUserWithRoles.DockerManagerUser.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, dockerManagerUserWithRoles.DockerManagerUser.Email!),
            new Claim(JwtRegisteredClaimNames.Name, dockerManagerUserWithRoles.DockerManagerUser.UserName!),
            new Claim("UserId", dockerManagerUserWithRoles.DockerManagerUser.Id!),
        ];
        claims.AddRange(dockerManagerUserWithRoles.Roles.Select(role => new Claim(ClaimTypes.Role, role)));


        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtOptions.ExpirationMinutes)),
            signingCredentials: signingCredentials
        );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return encodedToken;
    }

    // public List<string> ValidateToken(string? token)
    // {
    //     if (token == null)
    //     {
    //         return [];
    //     }
    //
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //
    //     var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
    //     if (jwtOptions is null)
    //     {
    //         throw new InvalidOperationException("JWT options are not configured in the application settings.");
    //     }
    //
    //     tokenHandler.ValidateToken(token, new TokenValidationParameters
    //     {
    //         ValidateIssuerSigningKey = true,
    //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
    //         ValidateIssuer = false,
    //         ValidateAudience = false,
    //         ClockSkew = TimeSpan.Zero
    //     }, out var validatedToken);
    //
    //     var jwtToken = (JwtSecurityToken)validatedToken;
    //     if (jwtToken == null)
    //     {
    //         return [];
    //     }
    //
    //     var roles = new List<string>();
    //     // ReSharper disable once LoopCanBeConvertedToQuery
    //     foreach (var claim in jwtToken.Claims)
    //     {
    //         if (claim.Type.Equals("role", StringComparison.CurrentCultureIgnoreCase))
    //         {
    //             roles.Add(claim.Value);
    //         }
    //     }
    //
    //     return roles;
    // }
}