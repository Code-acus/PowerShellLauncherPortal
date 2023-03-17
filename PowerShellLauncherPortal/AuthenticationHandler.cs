namespace PowerShellLauncherPortal
{
    public class AuthenticationHandler
    {
        private const string AzureAdClientId = "your-azuread-client-id";
        private const string AzureAdTenantId = "your-azuread-tenant-id";

        public async Task<bool> AuthenticateAsync()
        {
            var app = PublicClientApplicationBuilder.Create(AzureAdClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, AzureAdTenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            var scopes = new[] { "user.read" };

            try
            {
                var result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
                return result != null && !string.IsNullOrEmpty(result.AccessToken);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}