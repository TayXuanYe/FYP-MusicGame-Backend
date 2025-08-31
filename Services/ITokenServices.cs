// ITokenServices.cs
using System.Security.Claims;

public interface ITokenService
{
    string GenerateJwtToken(IEnumerable<Claim> claims);
}