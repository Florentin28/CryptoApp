namespace CryptoApp.Models;

public class Crypto
{
    public string Id { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    // On utilise "JsonPropertyName" car l'API envoie du snake_case (ex: current_price)
    [System.Text.Json.Serialization.JsonPropertyName("current_price")]
    public decimal CurrentPrice { get; set; }

    public string Image { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("market_cap_rank")]
    public int MarketCapRank { get; set; }
}