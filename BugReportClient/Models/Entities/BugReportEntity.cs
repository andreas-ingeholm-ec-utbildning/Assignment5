using System;
using System.ComponentModel.DataAnnotations;

namespace BugReportClient.Models.Entities;

public class BugReportEntity
{

    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;

}
