using System;

namespace IssuesClient.Models;

public class User
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

}