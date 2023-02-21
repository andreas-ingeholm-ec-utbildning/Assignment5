using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BugReportClient.Models.Entities;

[Index(nameof(EmailAddress), IsUnique = true)]
public class UserEntity
{

    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string EmailAddress { get; set; } = null!;

    [Required]
    public Guid AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;

    public ICollection<BugReportEntity> BugReports { get; set; } = new HashSet<BugReportEntity>();

}
