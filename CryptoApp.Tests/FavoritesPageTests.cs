using Bunit;
using Bunit.TestDoubles; 
using Xunit;
using CryptoApp.Pages;
using CryptoApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Tests;

public class FavoritesPageTests : BunitContext
{
    [Fact]
    public void PageFavoris_UtilisateurDeconnecte_AfficheMessageErreur()
    {
        // 1. Arrange
        // La méthode s'appelle maintenant AddAuthorization() dans les nouvelles versions
        var authContext = this.AddAuthorization();
        authContext.SetNotAuthorized(); 
        
        this.JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddScoped<FavoriteService>();

        // 2. Act
        var page = Render<Favorites>();

        // 3. Assert
        Assert.Contains("Vous devez être connecté pour voir cette page.", page.Markup);
    }
}