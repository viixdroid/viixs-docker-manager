using System.ComponentModel.DataAnnotations;

namespace DockerManager.Auth.Models.Pages;

public sealed class LoginModel
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Remember me?")] 
    public bool RememberMe { get; set; } = false;
}