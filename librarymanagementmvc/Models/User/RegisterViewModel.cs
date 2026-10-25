using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.MVC.ViewModels.User;

public class RegisterViewModel
{
  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  [MaxLength(200)]
  public string Email { get; set; } = string.Empty;

  [Required]
  public string Phone { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Password)]
  [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
  public string ConfirmPassword { get; set; } = string.Empty;
}
