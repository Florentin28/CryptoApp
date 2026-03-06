using Bunit;
using Xunit;
using CryptoApp.Services;

namespace CryptoApp.Tests;

// On utilise BunitContext pour simuler facilement le navigateur
public class FakeAuthStateProviderTests : BunitContext
{
    [Fact]
    public async Task Login_DoitConnecterLUtilisateur()
    {
        // 1. Arrange
        this.JSInterop.Mode = JSRuntimeMode.Loose; // Autorise l'usage du LocalStorage simulé
        var authProvider = new FakeAuthStateProvider(this.JSInterop.JSRuntime);

        // 2. Act
        await authProvider.LoginAsync("Jean Michel");
        var state = await authProvider.GetAuthenticationStateAsync();

        // 3. Assert
        Assert.True(state.User.Identity?.IsAuthenticated);
        Assert.Equal("Jean Michel", state.User.Identity?.Name);
    }
}