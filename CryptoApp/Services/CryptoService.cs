using System.Net.Http.Json;
using CryptoApp.Models;

namespace CryptoApp.Services;

public class CryptoService
{
    private readonly HttpClient _httpClient;

    public CryptoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CryptoCoin>?> GetTop10CryptosAsync()
    {
        // L'URL de CoinGecko pour récupérer le top 10 des cryptos (en dollars USD)
        string url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false";
        
        // On demande à HttpClient d'aller chercher les données et de les transformer en une liste de notre modèle CryptoCoin
        return await _httpClient.GetFromJsonAsync<List<CryptoCoin>>(url);
    }
}