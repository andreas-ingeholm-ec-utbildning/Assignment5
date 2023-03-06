using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IssuesClient.Models.Entities;

public class UserEntity
{

    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string EmailAddress { get; set; } = null!;
    [Required] public byte[]? Password { get; private set; }
    [Required] public byte[]? SecurityStamp { get; private set; }

    //public ICollection<ReportEntity> Reports { get; set; } = new HashSet<ReportEntity>();
    public ICollection<UserProfileEntity> Profiles { get; set; } = new HashSet<UserProfileEntity>();

    public void SetSecurePassword(string password)
    {
        using var hmac = new HMACSHA512();
        SecurityStamp = hmac.Key;
        Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool ValidatePassword(string password)
    {

        if (SecurityStamp is null || !SecurityStamp.Any())
            return false;

        using var hmac = new HMACSHA512();
        hmac.Key = SecurityStamp;
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)) == Password;

    }

    public static implicit operator UserEntity(User model) =>
        new()
        {
            Id = model.Id,
            EmailAddress = model.EmailAddress,
        };

    public static implicit operator User(UserEntity entity) =>
        new()
        {
            Id = entity.Id,
            EmailAddress = entity.EmailAddress,
        };

}
