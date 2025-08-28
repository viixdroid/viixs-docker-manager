using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DockerManager.Auth.Models.Pages;

public sealed class RegisterModel
{
    [Required]
    [EmailAddress]
    [Display(Name = nameof(Email))]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = nameof(Password))]
    public string Password { get; set; } = "";

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = "";
}