namespace DockerManager.Auth.Models.Settings;

public record JwtOptions(string Secret, string Issuer, string Audience, int ExpirationMinutes);