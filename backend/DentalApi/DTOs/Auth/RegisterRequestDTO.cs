using System.ComponentModel.DataAnnotations;

namespace DentalApi.DTOs.Auth;

public class RegisterRequestDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public string? CRO { get; set; }
    public string? Specialty { get; set; }
    public string? Phone { get; set; }
    public string Role { get; set; } = "Dentist";
}
