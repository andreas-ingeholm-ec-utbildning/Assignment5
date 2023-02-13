using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugReportClient.Models;
using BugReportClient.Models.Entities;
using Microsoft.Data.SqlClient;

namespace BugReportClient.Services;

public static class UserService
{

    private static readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\andre\Projects\Assignment5\BugReportClient\Data\db.mdf;Integrated Security=True;Connect Timeout=30";

    /// <summary>Saves the user to db.</summary>
    /// <returns>The id of the saved user.</returns>
    /// <exception cref="InvalidOperationException"/>
    public static async Task Save(User user)
    {

        var entity = new UserEntity
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            AddressId = await GetOrSaveAddress(user.Address)
        };

        await SaveUser(entity);

    }

    /// <summary>Saves the address to db.</summary>
    /// <returns>The id of the saved address.</returns>
    /// <exception cref="InvalidOperationException"/>
    static async Task<int> GetOrSaveAddress(Address address)
    {

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand(
            connection: connection,
            cmdText:
            " IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)" +
            " INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @StreetNumber, @PostalCode, @City)" +
            " ELSE" +
            " SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City");

        _ = command.Parameters.AddWithValue("@StreetName", address.StreetName);
        _ = command.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
        _ = command.Parameters.AddWithValue("@PostalCode", address.PostalCode);
        _ = command.Parameters.AddWithValue("@City", address.City);

        try
        {
            var result = await command.ExecuteScalarAsync();

            return result is int i
                ? i
                : throw new InvalidOperationException("An unknown error occured.\n\nExpected: (int)\nReceived: " + result?.ToString() ?? "(null)");

        }
        catch (Exception e)
        {

        }

        return 0;

    }

    /// <summary>Saves the user to db.</summary>
    /// <returns>The id of the saved user.</returns>
    /// <exception cref="InvalidOperationException"/>
    static async Task SaveUser(UserEntity user)
    {

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand(
        connection: connection,
        cmdText:
            " IF NOT EXISTS(SELECT Id FROM Users WHERE EmailAddress = @EmailAddress) " +
            " INSERT INTO Users OUTPUT INSERTED.Id VALUES(@FirstName, @LastName, @EmailAddress, @AddressId)");

        _ = command.Parameters.AddWithValue("@FirstName", user.FirstName);
        _ = command.Parameters.AddWithValue("@LastName", user.LastName);
        _ = command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
        _ = command.Parameters.AddWithValue("@AddressId", user.AddressId);

        try
        {
            _ = await command.ExecuteNonQueryAsync();

        }
        catch (Exception e)
        {

            throw;
        }


    }

    public static IEnumerable<User> ListUsers()
    {

        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = new SqlCommand(
            connection: connection,
            cmdText:
            " SELECT u.Id, u.FirstName, u.LastName, u.EmailAddress, a.StreetName, a.StreetNumber, a.PostalCode, a.City" +
            " FROM Users u" +
            " JOIN Addresses a ON u.AddressId = a.id");

        var result = command.ExecuteReader();

        while (result.Read())
            yield return new User()
            {
                Id = result.GetInt32(0),
                FirstName = result.GetString(1),
                LastName = result.GetString(2),
                EmailAddress = result.GetString(3),
                Address = new Address()
                {
                    StreetName = result.GetString(4),
                    StreetNumber = result.GetInt32(5),
                    PostalCode = result.GetString(6),
                    City = result.GetString(7),
                }
            };

    }

    public static async Task<User?> GetUser(string emailAddress)
    {

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var command = new SqlCommand(
            connection: connection,
            cmdText:
            " SELECT u.Id, u.FirstName, u.LastName, u.EmailAddress, a.StreetName, a.StreetNumber, a.PostalCode, a.City" +
            " FROM Users u" +
            " JOIN Addresses a ON u.AddressId = a.id" +
            " WHERE u.Email = @Email");

        _ = command.Parameters.AddWithValue("@EmailAddress", emailAddress);

        var result = await command.ExecuteReaderAsync();

        if (!result.HasRows)
            return null;

        var user = new User();
        while (await result.ReadAsync())
        {
            user.Id = result.GetInt32(0);
            user.FirstName = result.GetString(1);
            user.LastName = result.GetString(2);
            user.EmailAddress = result.GetString(3);
            user.Address = new Address()
            {
                StreetName = result.GetString(4),
                StreetNumber = result.GetInt32(5),
                PostalCode = result.GetString(6),
                City = result.GetString(7),
            };
        }

        return user;

    }

}
