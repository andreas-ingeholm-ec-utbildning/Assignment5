using System;

namespace IssuesClient.Models;

public class Comment
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Content { get; set; } = null!;
    public DateTime Created { get; set; }

}