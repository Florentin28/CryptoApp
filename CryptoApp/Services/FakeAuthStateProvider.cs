using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CryptoApp.Services;

public class FakeAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;

    public FakeAuthStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // On vérifie si un nom est sauvegardé dans le navigateur
            var username = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
            
            if (!string.IsNullOrEmpty(username))
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
        }
        catch { /* Ignore les erreurs au chargement initial */ }

        // Si personne, on renvoie un visiteur anonyme
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task LoginAsync(string username)
    {
        // On sauvegarde le profil dans le navigateur
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", username);
        
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task LogoutAsync()
    {
        // On vide le navigateur à la déconnexion
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "currentUser");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
}