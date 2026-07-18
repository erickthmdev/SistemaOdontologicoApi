using DentalApi.DTOs.Auth;
using DentalApi.DTOs.User;

namespace DentalApi.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO dto);
    Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO dto);
    Task<UserResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserResponseDTO>> GetAllAsync();
}
