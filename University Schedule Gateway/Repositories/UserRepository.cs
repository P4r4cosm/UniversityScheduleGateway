using Microsoft.EntityFrameworkCore;
using University_Schedule_Gateway.Data;
using University_Schedule_Gateway.Models;

namespace University_Schedule_Gateway.Repositories;

public class UserRepository
{
    private readonly AuthenticationContext _context;

    public UserRepository(AuthenticationContext dbContext)
        => _context = dbContext;

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByName(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
    }
}