using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IssuesClient.Models.Entities;

[Index(nameof(EmailAddress), IsUnique = true)]
public class UserEntity
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public int PhoneNumber { get; set; }

    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public string Password { get; set; } = null!;
    public bool IsValidated { get; set; }
    public string SecurityStamp { get; set; } = null!;

    public string NormalizedEmailAddress => EmailAddress.ToUpper();

    public ICollection<ReportEntity> Reports { get; set; } = new HashSet<ReportEntity>();

    public static implicit operator User(UserEntity entity) =>
        new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            PhoneNumber = entity.PhoneNumber,
        };

}
