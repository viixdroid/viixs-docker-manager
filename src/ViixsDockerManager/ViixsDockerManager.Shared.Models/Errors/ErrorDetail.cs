namespace ViixsDockerManager.Shared.Models.Errors;

/// <summary>
/// Represents an error with its code, description, and category.
/// </summary>
/// <param name="Code"></param>
/// <param name="Description"></param>
/// <param name="Category"></param>
public sealed record ErrorDetail(string Code, string Description, string Category);
