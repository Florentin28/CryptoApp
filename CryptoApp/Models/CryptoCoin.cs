using System.Text.Json.Serialization;

namespace CryptoApp.Models;

public class CryptoCoin
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("current_price")]
    public decimal CurrentPrice { get; set; }

    [JsonPropertyName("image")]
    public string ImageUrl { get; set; } = string.Empty;
}