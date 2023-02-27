using System;

namespace IssuesClient.Models;

public class Comment
{

    public Guid Id { get; set; }

    public User User { get; set; } = null!;
    public string? Content { get; set; }
    public bool IsRemoved { get; set; }

}