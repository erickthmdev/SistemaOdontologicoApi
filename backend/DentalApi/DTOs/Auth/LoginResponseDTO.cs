using DentalApi.DTOs.User;

namespace DentalApi.DTOs.Auth;

public class LoginResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserResponseDTO User { get; set; } = new();
}
