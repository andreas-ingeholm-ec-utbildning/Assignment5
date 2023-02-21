using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugReportClient.Contexts;
using BugReportClient.Models;
using BugReportClient.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugReportClient.Services;

public static class UserService
{

    static readonly DataContext _context = new();

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
        entity.Address = (await FindAddressEntityInDB(user)) ?? user.ToAddressEntity();

        _ = _context.Add(entity);
        _ = await _context.SaveChangesAsync();

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
        entity.Address = (await FindAddressEntityInDB(user)) ?? user.ToAddressEntity();

        _ = _context.Update(entity);
        _ = await _context.SaveChangesAsync();

    }

    #endregion
    #region Get

    /// <summary>Used by AddUserPopup for filling test data.</summary>
    public static int CachedListCount { get; private set; }

    public static async Task<IEnumerable<User>> GetAllAsync()
    {
        var list = (await _context.Users.Include(u => u.Address).ToArrayAsync()).Select(ToModel).OfType<User>();
        CachedListCount = list.Count();
        return list;
    }

    public static async Task<User?> GetAsync(string emailAddress) =>
        !string.IsNullOrEmpty(emailAddress)
        ? ((await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.EmailAddress == emailAddress))?.ToModel())
        : null;

    public static async Task<User?> GetAsync(Guid id) =>
        id != Guid.Empty
        ? ((await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id))?.ToModel())
        : null;

    static async Task<AddressEntity?> FindAddressEntityInDB(this User user) =>
        await _context.Addresses.FirstOrDefaultAsync(a => a.StreetName == user.StreetName && a.StreetNumber == user.StreetNumber && a.City == user.City && a.PostalCode == user.PostalCode);

    #endregion
    #region Delete

    public static Task DeleteAsync(User user) =>
        DeleteAsync(user.EmailAddress);

    public static async Task DeleteAsync(string emailAddress)
    {

        if (string.IsNullOrEmpty(emailAddress))
            return;

        var user = await GetAsync(emailAddress);
        if (user is not null)
        {
            _ = _context.Remove(user);
            _ = _context.SaveChangesAsync();
        }

    }

    #endregion
    #region To/from entity/model

    static Address ToModel(this AddressEntity entity) =>
        new() { Id = entity.Id, StreetName = entity.StreetName, StreetNumber = entity.StreetNumber, City = entity.City, PostalCode = entity.PostalCode };

    static User? ToModel(this UserEntity? entity) =>
        entity is not null
        ? new()
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            StreetName = entity.Address?.StreetName ?? string.Empty,
            StreetNumber = entity.Address?.StreetNumber ?? -1,
            City = entity.Address?.City ?? string.Empty,
            PostalCode = entity.Address?.PostalCode ?? string.Empty
        }
        : null;

    static AddressEntity ToAddressEntity(this User user) =>
        new() { StreetName = user.StreetName, StreetNumber = user.StreetNumber, City = user.City, PostalCode = user.PostalCode };

    static UserEntity ToEntity(this User user) =>
        new() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, EmailAddress = user.EmailAddress };

    #endregion

}
