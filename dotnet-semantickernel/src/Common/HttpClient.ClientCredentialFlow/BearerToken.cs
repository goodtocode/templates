using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoodToCode.HttpClient.ClientCredentialFlow;

public class BearerToken
{
    private readonly ClientSetting _accessTokenSetting;

    public BearerToken(ClientSetting accessTokenSetting)
    {
        _accessTokenSetting = accessTokenSetting;
    }

    private string Token { get; set; } = string.Empty;

    private DateTime ExpirationDateUtc { get; set; }

    private bool TokenIsExpired
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Token))
                return true;

            var timeDif = ExpirationDateUtc - DateTime.UtcNow;
            var minutesUntilExpiration = timeDif.TotalMinutes;
            return minutesUntilExpiration < 1;
        }
    }

    public async Task<string> GetAccessTokenAsync()
    {
        if (TokenIsExpired)
            Token = await GetNewAccessToken();

        return Token;
    }

    private async Task<string> GetNewAccessToken()
    {
        var httpClient = new System.Net.Http.HttpClient();
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "scope", _accessTokenSetting.Scope },
            { "client_id", _accessTokenSetting.ClientId },
            { "client_secret", _accessTokenSetting.ClientSecret },
            { "grant_type", "client_credentials" }
        });

        var response = await httpClient.PostAsync(_accessTokenSetting.TokenUrl, content);
        if (!response.IsSuccessStatusCode) return string.Empty;
        var tokenResponse = JsonSerializer.Deserialize<BearerTokenDto>(await response.Content.ReadAsStringAsync());
        ExpirationDateUtc = DateTime.UtcNow.AddSeconds(tokenResponse?.ExpiresIn ?? 3600);
        return tokenResponse?.AccessToken ?? string.Empty;
    }

    private class BearerTokenDto
    {
        [JsonPropertyName("token_type")] public string? TokenType { get; set; }

        [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

        [JsonPropertyName("ext_expires_in")] public int ExtExpiresIn { get; set; }

        [JsonPropertyName("access_token")] public string? AccessToken { get; set; }
    }
}