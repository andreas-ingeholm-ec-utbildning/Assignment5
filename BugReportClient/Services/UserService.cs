using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugReportClient.Models;
using BugReportClient.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static BugReportClient.Services.DBService;

namespace BugReportClient.Services;

public static class UserService
{

    #region Get

    /// <summary>Used by AddUserPopup for filling test data.</summary>
    public static int CachedListCount { get; private set; }

    public static async Task<IEnumerable<User>> GetAllAsync()
    {
        var list = (await Context.Users.Include(u => u.Address).ToArrayAsync()).Select(ToModel).OfType<User>();
        CachedListCount = list.Count();
        return list;
    }

    public static async Task<User?> GetAsync(string emailAddress) =>
        !string.IsNullOrEmpty(emailAddress)
        ? ((await Context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.EmailAddress == emailAddress))?.ToModel())
        : null;

    public static async Task<User?> GetAsync(Guid id) =>
        id != Guid.Empty
        ? ((await Context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id))?.ToModel())
        : null;

    public static async Task<UserEntity?> GetEntityAsync(string emailAddress) =>
        await Context.Users.FirstOrDefaultAsync(a => a.EmailAddress == emailAddress);

    public static async Task<UserEntity?> GetEntityAsync(User user) =>
              await Context.Users.FirstOrDefaultAsync(a => a.Id == user.Id);

    #endregion
    #region Save

    public static async Task SaveAsync(User user)
    {

        if (await GetAsync(user.Id) is User existing)
            await UpdateAsync(user, existing);
        else
            await CreateAsync(user);

    }

    static async Task CreateAsync(User user)
    {

        var entity = user.ToEntity();
        entity.Address = (await AddressService.GetEntityAsync(user)) ?? user.ToAddressEntity();

        _ = Context.Add(entity);
        _ = await Context.SaveChangesAsync();

    }

    static async Task UpdateAsync(User user, User existingUser)
    {

        user.FirstName ??= existingUser.FirstName;
        user.LastName ??= existingUser.LastName;
        user.EmailAddress ??= existingUser.EmailAddress;
        user.StreetName ??= existingUser.StreetName;
        user.StreetNumber ??= existingUser.StreetNumber;
        user.City ??= existingUser.City;
        user.PostalCode ??= existingUser.PostalCode;

        var entity = user.ToEntity();
        entity.Address = (await AddressService.GetEntityAsync(user)) ?? user.ToAddressEntity();

        _ = Context.Update(entity);
        _ = await Context.SaveChangesAsync();

    }

    #endregion
    #region Delete

    public static Task DeleteAsync(User user) =>
        DeleteAsync(user.EmailAddress);

    public static async Task DeleteAsync(string emailAddress)
    {

        if (string.IsNullOrEmpty(emailAddress))
            return;

        var user = await GetEntityAsync(emailAddress);
        if (user is not null)
        {
            _ = Context.Remove(user);
            _ = await Context.SaveChangesAsync();
        }

    }

    #endregion
    #region To/from entity/model

    public static User? ToModel(this UserEntity? entity) =>
         entity is not null
         ? new()
         {
             Id = entity.Id,
             FirstName = entity.FirstName,
             LastName = entity.LastName,
             EmailAddress = entity.EmailAddress,
             StreetName = entity.Address?.StreetName ?? string.Empty,
             StreetNumber = entity.Address?.StreetNumber ?? -1,
             City = entity.Address?.City ?? string.Empty,
             PostalCode = entity.Address?.PostalCode ?? string.Empty
         }
         : null;

    public static UserEntity ToEntity(this User model) =>
        new() { Id = model.Id, FirstName = model.FirstName, LastName = model.LastName, EmailAddress = model.EmailAddress };

    #endregion

}
