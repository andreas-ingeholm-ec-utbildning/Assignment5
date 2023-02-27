using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssuesClient.Models.Entities;

public enum ReportStatus
{
    Pending,
    InProgress,
    Completed
}

public class ReportEntity
{

    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public ReportStatus Status { get; set; }
    [Required] public IEnumerable<CommentEntity> Comments { get; set; } = null!;

    [Required] public Guid UserId { get; set; }
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
