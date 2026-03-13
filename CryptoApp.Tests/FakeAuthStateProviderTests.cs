using Bunit;
using Xunit;
using CryptoApp.Services;
using Microsoft.JSInterop;

namespace CryptoApp.Tests;

public class FakeAuthStateProviderTests : BunitContext
{
    [Fact]
    public async Task Login_DoitConnecterLUtilisateur()
    {
        // 1. Arrange
        this.JSInterop.Mode = JSRuntimeMode.Loose; 

        // ON UTILISE LA MÊME CLÉ QUE DANS LE SERVICE : "currentUser"
        this.JSInterop.Setup<string>("localStorage.getItem", "currentUser")
                      .SetResult("Jean Michel");

        var authProvider = new FakeAuthStateProvider(this.JSInterop.JSRuntime);

        // 2. Act
        await authProvider.LoginAsync("Jean Michel");
        
        // On récupère l'état (qui va aller lire "currentUser" dans le faux LocalStorage)
        var state = await authProvider.GetAuthenticationStateAsync();

        // 3. Assert
        Assert.True(state.User.Identity?.IsAuthenticated);
        Assert.Equal("Jean Michel", state.User.Identity?.Name);
    }
}