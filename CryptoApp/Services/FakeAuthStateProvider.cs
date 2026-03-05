using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace CryptoApp.Services;

// On hérite de la classe officielle de Blazor pour l'authentification
public class FakeAuthStateProvider : AuthenticationStateProvider
{
    // Un utilisateur vide (non connecté)
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    
    // Un utilisateur factice (connecté)
    private readonly ClaimsPrincipal _user = new(new ClaimsIdentity(
    [
        new Claim(ClaimTypes.Name, "Étudiant")
    ], "FakeAuth"));

    private bool _isAuthenticated = false;

    // Cette méthode est appelée par Blazor pour savoir si on est connecté
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = _isAuthenticated ? _user : _anonymous;
        return Task.FromResult(new AuthenticationState(principal));
    }

    // Méthode pour se connecter
    public void Login()
    {
        _isAuthenticated = true;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    // Méthode pour se déconnecter
    public void Logout()
    {
        _isAuthenticated = false;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}