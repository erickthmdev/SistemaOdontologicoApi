using DentalApi.Data;
using DentalApi.Models;
using DentalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentalApi.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var lowered = (email ?? string.Empty).ToLower();
        return await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower() == lowered);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var lowered = (email ?? string.Empty).ToLower();
        return await _dbSet.AnyAsync(u => u.Email.ToLower() == lowered);
    }
}
