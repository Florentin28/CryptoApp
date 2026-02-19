using System.Net.Http.Json;
using CryptoApp.Models;

namespace CryptoApp.Services;

public class CryptoService
{
    private readonly HttpClient _http;

    public CryptoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Crypto>> GetTop10Cryptos()
    {
        var url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1";
        var result = await _http.GetFromJsonAsync<List<Crypto>>(url);
        return result ?? new List<Crypto>();
    }
}