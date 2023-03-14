using System;
using System.ComponentModel.DataAnnotations;
using IssuesClient.Services;

namespace IssuesClient.Models.Entities;

public class UserProfileEntity
{

    [Required] public Guid Id { get; set; }

    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public string PhoneNumber { get; set; } = null!;

    public UserEntity User { get; set; } = null!;

    public static implicit operator User?(UserProfileEntity? entity) =>
        entity is null
        ? null
        : new()
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
            User = DBService.Context.Users.Entry(model).Entity
        };

}