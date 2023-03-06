using System;
using System.ComponentModel.DataAnnotations;

namespace IssuesClient.Models.Entities;

public class UserProfileEntity
{

    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public string PhoneNumber { get; set; } = null!;

    public UserEntity User { get; set; } = null!;

    public static implicit operator User(UserProfileEntity entity) =>
        new()
        {
            Id = entity.User.Id,
            EmailAddress = entity.User.EmailAddress,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhoneNumber = entity.PhoneNumber,
        };

    public static implicit operator UserProfileEntity(User model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            User = model
        };

}