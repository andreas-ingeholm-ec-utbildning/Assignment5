using System;

namespace IssuesClient.Models;

public class User
{

    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public int PhoneNumber { get; set; }

}