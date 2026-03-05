using System.Text.Json;
using CryptoApp.Models;
using Microsoft.JSInterop;

namespace CryptoApp.Services;

public class FavoriteService
{
    private readonly IJSRuntime _jsRuntime;
    private const string Key = "mes_favoris"; // Le nom de notre "tiroir" dans le navigateur

    public FavoriteService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // 1. Lire les favoris
    public async Task<List<CryptoCoin>> GetFavoritesAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", Key);
        if (string.IsNullOrEmpty(json)) return new List<CryptoCoin>();
        
        // On transforme le texte du LocalStorage en vraie liste d'objets C#
        return JsonSerializer.Deserialize<List<CryptoCoin>>(json) ?? new List<CryptoCoin>();
    }

    // 2. Sauvegarder les favoris
    public async Task SaveFavoritesAsync(List<CryptoCoin> favorites)
    {
        var json = JsonSerializer.Serialize(favorites);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", Key, json);
    }

    // 3. Ajouter un favori
    public async Task AddFavoriteAsync(CryptoCoin coin)
    {
        var favorites = await GetFavoritesAsync();
        if (!favorites.Any(f => f.Id == coin.Id)) // On vérifie qu'il n'y est pas déjà
        {
            favorites.Add(coin);
            await SaveFavoritesAsync(favorites);
        }
    }

    // 4. Supprimer un favori
    public async Task RemoveFavoriteAsync(string id)
    {
        var favorites = await GetFavoritesAsync();
        favorites.RemoveAll(f => f.Id == id);
        await SaveFavoritesAsync(favorites);
    }

    // 5. Modifier un favori (pour respecter la consigne "modifier les données")
    public async Task UpdateFavoriteAsync(CryptoCoin coin)
    {
        var favorites = await GetFavoritesAsync();
        var index = favorites.FindIndex(f => f.Id == coin.Id);
        if (index != -1)
        {
            favorites[index] = coin; // On écrase l'ancien par le nouveau modifié
            await SaveFavoritesAsync(favorites);
        }
    }
}