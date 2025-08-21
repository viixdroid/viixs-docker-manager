using System.Security.Claims;
using DockerManager.Auth.Models.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.JsonWebTokens;
using static DockerManager.Shared.Constants.ApplicationConstants.Authentication;

namespace DockerManager.Auth.Providers;

public class CustomAuthenticationProvider(ProtectedLocalStorage protectedLocalStorage) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var getTokenResult = await protectedLocalStorage.GetAsync<UserLoggedInModel>(TokenStorageKey);
        if (!getTokenResult.Success || getTokenResult.Value is null)
        {
            return new AuthenticationState(_anonymousUser);
        }

        var userLoggedInModel = getTokenResult.Value;
        if (userLoggedInModel is null)
        {
            return new AuthenticationState(_anonymousUser);
        }

        var claims = GetClaimsFromJsonWebToken(userLoggedInModel.Token).ToList();
        if (claims.Count == 0)
        {
            return new AuthenticationState(_anonymousUser);
        }
        var identity = new ClaimsIdentity(claims, JsonWebTokenAuthenticationType);
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    // TODO: move to a more appropriate class
    public void AuthenticateUser(string token)
    {
        var claims = GetClaimsFromJsonWebToken(token).ToList();
        var identity = new ClaimsIdentity(claims, JsonWebTokenAuthenticationType);
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    // TODO: move to a more appropriate class
    private static IEnumerable<Claim> GetClaimsFromJsonWebToken(string token)
    {
        var tokenHandler = new JsonWebTokenHandler();
        if (!tokenHandler.CanReadToken(token))
        {
            return [];
        }

        var readToken = tokenHandler.ReadJsonWebToken(token);

        return readToken.Claims;
    }
}
