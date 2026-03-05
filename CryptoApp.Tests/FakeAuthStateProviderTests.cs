using Xunit;
using CryptoApp.Services;

namespace CryptoApp.Tests;

public class FakeAuthStateProviderTests
{
    // On met la méthode en "async Task" au lieu de "void"
    [Fact]
    public async Task Login_DoitConnecterLUtilisateur()
    {
        // 1. Arrange
        var authProvider = new FakeAuthStateProvider();

        // 2. Act
        authProvider.Login();
        
        // On utilise "await" pour récupérer l'état de façon moderne et fluide
        var state = await authProvider.GetAuthenticationStateAsync();

        // 3. Assert
        Assert.True(state.User.Identity?.IsAuthenticated);
        Assert.Equal("Étudiant", state.User.Identity?.Name);
    }
}