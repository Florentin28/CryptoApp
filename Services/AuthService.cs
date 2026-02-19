namespace CryptoApp.Services;

public class AuthService
{
    public string? CurrentUser { get; private set; }
    public bool IsAuthenticated => !string.IsNullOrEmpty(CurrentUser);

    public void Login(string username) => CurrentUser = username;
    public void Logout() => CurrentUser = null;
}