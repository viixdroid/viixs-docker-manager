namespace DockerManager.Shared.Constants;

public static class ApplicationConstants
{
    public const string ApplicationName = "Viix's Docker Manager";

    public static class DataProtection
    {
        public const string DockerKeysFolder = @"/app/keys";

        public static string GetWindowsKeysFolder()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppData, ApplicationName, "DataProtection");
        }
    }

    public static class Authentication
    {
        public const string JsonWebTokenAuthenticationType = "JsonWebToken";
        public const string TokenStorageKey = "tokenStorage";
    }

    public const string BackendApiHttpClientName = "BackendApiHttpClient";
}
