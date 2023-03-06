using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssuesClient.Models.Entities;

public enum ReportStatus
{
    Pending,
    InProgress,
    Completed
}

public class ReportEntity
{

    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public ReportStatus Status { get; set; }

    [Required]
    [ForeignKey("Comments")]
    public IEnumerable<CommentEntity> CommentIds { get; set; } = null!;

    [Required]
    public UserEntity User { get; set; } = null!;

    public static implicit operator Report(ReportEntity entity) =>
        new()
        {
            Id = entity.Id,
            Status = entity.Status,
            //Comments = entity.Comments,
            User = entity.User,
        };

}
