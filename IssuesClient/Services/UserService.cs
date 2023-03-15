using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuesClient.Models;
using IssuesClient.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static IssuesClient.Services.DBService;

namespace IssuesClient.Services;

static class UserService
{

    /// <summary>Used for filling debug data.</summary>
    public static int CachedCount { get; private set; }

    public static async Task CreateAsync(User user)
    {
        _ = await Context.Profiles.AddAsync(user);
        _ = await Context.SaveChangesAsync();
    }

    public static async Task UpdateAsync(User user)
    {
        _ = Context.Profiles.Update(user);
        _ = await Context.SaveChangesAsync();
    }

    public static async Task RemoveAsync(User user)
    {
        if (await Context.Profiles.Include(p => p.User).FirstOrDefaultAsync(p => p.User.Id == user.Id) is UserProfileEntity entity)
        {
            _ = Context.Profiles.Remove(entity);
            _ = await Context.SaveChangesAsync();
        }
    }

    public static async Task<User?> GetAsync(Guid id) =>
       await Context.Profiles.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

    public static async Task<IEnumerable<User>> GetAllAsync()
    {
        var list =
            (await Context.Profiles.Include(p => p.User).ToArrayAsync()).
            Select(u => (User?)u).
            OfType<User>().
            OrderBy(u => u.EmailAddress);

        CachedCount = list.Count();
        return list;

    }

    public static UserProfileEntity GetProfileEntity(User user) =>
        Context.Profiles.Entry(user).Entity;

    public static UserEntity GetUserEntity(User user) =>
        Context.Users.Entry(user).Entity;

}