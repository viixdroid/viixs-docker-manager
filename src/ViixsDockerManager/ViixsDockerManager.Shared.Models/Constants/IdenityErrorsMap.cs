using ViixsDockerManager.Shared.Models.Errors;

namespace ViixsDockerManager.Shared.Models.Constants;

/// <summary>
/// Provides a mapping of Identity error codes to meaningful descriptions, categorized by type.
/// </summary>
internal static class IdenityErrorsMap
{
    /// <summary>
    /// A list of Identity errors categorized by type.
    /// </summary>
    public static readonly IReadOnlyList<ErrorDetail> Errors =
    [
        // Email/Username-related errors
        new ErrorDetail("DuplicateUserName", "The email address is already associated with another account.", "Email"),
        new ErrorDetail("DuplicateEmail", "The email address is already associated with another account.", "Email"),
        new ErrorDetail("InvalidUserName", "The username contains invalid characters or does not meet the required format(name@provider.tld).","Email"),
        new ErrorDetail("InvalidEmail", "The email address is not in a valid format(name@provider.tld).", "Email"),

        // Password-related errors
        new ErrorDetail("PasswordTooShort", "The password is too short. It must meet the minimum length requirement.","Password"),
        new ErrorDetail("PasswordRequiresNonAlphanumeric", "The password must contain at least one non-alphanumeric character (e.g., !, @, #).", "Password"),
        new ErrorDetail("PasswordRequiresDigit", "The password must contain at least one numeric digit (0-9).", "Password"),
        new ErrorDetail("PasswordRequiresLower", "The password must contain at least one lowercase letter (a-z).", "Password"),
        new ErrorDetail("PasswordRequiresUpper", "The password must contain at least one uppercase letter (A-Z).", "Password"),
        new ErrorDetail ("PasswordRequiresUniqueChars", "The password must contain the required number of unique characters.", "Password"),

        // Role-related errors
        new ErrorDetail("UserAlreadyInRole", "The user is already assigned to the specified role.", "Role"),
        new ErrorDetail("UserNotInRole", "The user is not assigned to the specified role.", "Role"),

        // General errors
        new ErrorDetail("ConcurrencyFailure", "A concurrency conflict occurred. The resource was modified by another process.", "General"),
        new ErrorDetail("LoginAlreadyAssociated", "This login is already associated with another user account.", "General"),
        new ErrorDetail("InvalidToken", "The provided token is invalid or expired.", "General"),
        new ErrorDetail("RecoveryCodeRedemptionFailed", "The recovery code is invalid or has already been used.", "General"),
        new ErrorDetail("DefaultError", "An unknown error occurred. Please try again later.", "General")
    ];
}
