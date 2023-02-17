using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugReportClient.Models;
using BugReportClient.Models.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;

namespace BugReportClient.Services;

public static class UserService
{

    static readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\andre\Projects\Assignment5\BugReportClient\Data\db.mdf;Integrated Security=True;Connect Timeout=30";

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

        var entity = new UserEntity()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            AddressId = await SaveAsync(new AddressEntity()
            {
                StreetName = user.StreetName,
                StreetNumber = user.StreetNumber,
                City = user.City,
                PostalCode = user.PostalCode,
            })
        };

        using var connection = new SqlConnection(_connectionString);
        _ = await connection.ExecuteAsync(
            " IF NOT EXISTS(SELECT Id FROM Users WHERE EmailAddress = @EmailAddress) " +
            " INSERT INTO Users VALUES(@FirstName, @LastName, @EmailAddress, @AddressId)", entity);

    }

    static async Task UpdateAsync(User user, User existingUser)
    {

        var entity = new UserEntity()
        {
            Id = user.Id,
            FirstName = GetValue(user.FirstName, existingUser.FirstName),
            LastName = GetValue(user.LastName, existingUser.LastName),
            EmailAddress = GetValue(user.EmailAddress, existingUser.EmailAddress),
            AddressId = await SaveAsync(new AddressEntity()
            {
                StreetName = GetValue(user.StreetName, existingUser.StreetName),
                StreetNumber = user.StreetNumber ?? existingUser.StreetNumber,
                City = GetValue(user.City, existingUser.City),
                PostalCode = GetValue(user.PostalCode, existingUser.PostalCode),
            })
        };

        using var connection = new SqlConnection(_connectionString);
        _ = await connection.ExecuteAsync(
            " UPDATE Users SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, AddressId = @AddressId" +
            " WHERE Id = @Id", entity);

        static string GetValue(string newValue, string oldValue) =>
            string.IsNullOrEmpty(newValue)
            ? oldValue
            : newValue;

    }

    /// <summary>Saves the <see cref="AddressEntity"/>, if it does already not exist.</summary>
    /// <returns>Created / existing id of address in databse.</returns>
    static async Task<int> SaveAsync(AddressEntity entity)
    {

        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(
            " IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)" +
            " INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @StreetNumber, @PostalCode, @City)" +
            " ELSE" +
            " SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City", entity);

    }

    #endregion
    #region Get

    public static int CachedListCount { get; private set; }
    public static async Task<IEnumerable<User>> GetAllAsync()
    {

        using var connection = new SqlConnection(_connectionString);
        var users = await connection.QueryAsync<User>(
            " SELECT u.Id, u.FirstName, u.LastName, u.EmailAddress, a.StreetName, a.StreetNumber, a.PostalCode, a.City" +
            " FROM Users u" +
            " JOIN Addresses a ON u.AddressId = a.id");

        CachedListCount = users.Count();
        return users;

    }

    public static async Task<User?> GetAsync(string emailAddress)
    {

        if (string.IsNullOrEmpty(emailAddress))
            return null;

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstAsync<User>(
            " SELECT u.Id, u.FirstName, u.LastName, u.EmailAddress, a.StreetName, a.StreetNumber, a.PostalCode, a.City" +
            " FROM Users u" +
            " JOIN Addresses a ON u.AddressId = a.id" +
            " WHERE u.Email = @Email", new { EmailAddress = emailAddress });

    }

    public static async Task<User?> GetAsync(int id)
    {

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstAsync<User?>(
            " SELECT u.Id, u.FirstName, u.LastName, u.EmailAddress, a.StreetName, a.StreetNumber, a.PostalCode, a.City" +
            " FROM Users u" +
            " JOIN Addresses a ON u.AddressId = a.id" +
            " WHERE u.Id = @Id", new { Id = id });

    }

    #endregion
    #region Delete

    public static Task DeleteAsync(User user) =>
        DeleteAsync(user?.EmailAddress ?? null);

    public static async Task DeleteAsync(string emailAddress)
    {

        if (string.IsNullOrEmpty(emailAddress))
            return;

        using var connection = new SqlConnection(_connectionString);
        _ = await connection.ExecuteAsync("DELETE FROM Users WHERE EmailAddress = @EmailAddress", new { EmailAddress = emailAddress });

    }

    #endregion

}
