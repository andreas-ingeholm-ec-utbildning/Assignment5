using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssuesClient.Models.Entities;

public class UserEntity
{

    [Required] public Guid Id { get; set; }

    [Required] public string EmailAddress { get; set; } = null!;

    public ICollection<UserProfileEntity> Profiles { get; set; } = new HashSet<UserProfileEntity>();

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
