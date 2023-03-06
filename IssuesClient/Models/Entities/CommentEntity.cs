using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssuesClient.Models.Entities;

public class CommentEntity
{

    [Required] public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsRemoved { get; set; }

    [Required] public string Content { get; set; } = null!;

    [Required] public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public ICollection<ReportEntity> Reports { get; set; } = new HashSet<ReportEntity>();

    public static implicit operator Comment(CommentEntity entity) =>
        new()
        {
            Id = entity.Id,
            Content = entity.Content,
            User = entity.User,
            IsRemoved = entity.IsRemoved,
        };

}
