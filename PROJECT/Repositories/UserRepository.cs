using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAll() =>
        await _context.Users.ToListAsync();

    public async Task<User?> GetById(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<bool> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> EmailExists(string email) =>
    await _context.Users.AnyAsync(u => u.Email == email);
    public async Task<User?> Update(int id, string name, string email)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Name = name;
        user.Email = email;

        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<bool> EmailExistsForOtherUser(int id, string email) =>
    await _context.Users.AnyAsync(u => u.Email == email && u.Id != id);

    public async Task<User?> GetByEmail(string email) =>
    await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
}