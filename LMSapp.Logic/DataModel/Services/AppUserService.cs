using Microsoft.EntityFrameworkCore;

public class AppUserService : IDataService<AppUser>
{
    private readonly AppDbContext _context;
    public AppUserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetByIdAsync(int appUserId)
    {
        return await _context.AppUsers.FirstOrDefaultAsync(u => u.AppUserId == appUserId);
    }

    public async Task<IEnumerable<AppUser>?> GetAllAsync()
    {
        return await _context.AppUsers.Where(u => u.IsDeleted == false).ToListAsync();
    }

    public async Task<AppUser?> GetByLoginAsync(string login)
    {
        return await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        var normalized = email.Trim();
        return await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == normalized);
    }

    public async Task<AppUser?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var normalizedEmail = email.Trim();
        return await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == normalizedEmail && u.Password == password);
    }

    public async Task<AppUser> CreateAsync(AppUser user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<AppUser?> UpdateAsync(AppUser user)
    {
        var userToUpdate = await _context.AppUsers.FindAsync(user.AppUserId);
        if (userToUpdate is null) return null;

        userToUpdate.FirstName = user.FirstName;
        userToUpdate.LastName = user.LastName;
        userToUpdate.Email = user.Email;
        userToUpdate.Phone = user.Phone;

        await _context.SaveChangesAsync();
        return userToUpdate;
    }

    public async Task<bool> DeleteAsync(int appUserId)
    {
        var userToDelete = await _context.AppUsers.Where(u => u.IsDeleted == false && u.AppUserId == appUserId).SingleOrDefaultAsync();
        if (userToDelete is null) return false;

        userToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
