using System.Text.Json;
using Microsoft.JSInterop;
using CryptoApp.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace CryptoApp.Services;

public class FavoriteService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    // On injecte l'état d'authentification ici !
    public FavoriteService(IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
    {
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
    }

    // Cette méthode génère la clé unique (ex: "favorites_Daniel")
    private async Task<string> GetUserKeyAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var username = authState.User.Identity?.Name ?? "Anonyme";
        return $"favorites_{username}";
    }

    public async Task<List<CryptoCoin>> GetFavoritesAsync()
    {
        var key = await GetUserKeyAsync();
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        if (string.IsNullOrEmpty(json)) return new List<CryptoCoin>();
        return JsonSerializer.Deserialize<List<CryptoCoin>>(json) ?? new List<CryptoCoin>();
    }

    public async Task AddFavoriteAsync(CryptoCoin coin)
    {
        var favorites = await GetFavoritesAsync();
        if (!favorites.Any(f => f.Id == coin.Id))
        {
            favorites.Add(coin);
            var key = await GetUserKeyAsync();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(favorites));
        }
    }

    public async Task RemoveFavoriteAsync(string id)
    {
        var favorites = await GetFavoritesAsync();
        var coinToRemove = favorites.FirstOrDefault(f => f.Id == id);
        if (coinToRemove != null)
        {
            favorites.Remove(coinToRemove);
            var key = await GetUserKeyAsync();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(favorites));
        }
    }

    public async Task UpdateFavoriteAsync(CryptoCoin coin)
    {
        var favorites = await GetFavoritesAsync();
        var existingCoin = favorites.FirstOrDefault(f => f.Id == coin.Id);
        if (existingCoin != null)
        {
            existingCoin.CurrentPrice = coin.CurrentPrice;
            var key = await GetUserKeyAsync();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(favorites));
        }
    }
}