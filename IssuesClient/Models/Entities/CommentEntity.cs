using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssuesClient.Models.Entities;

public class CommentEntity
{

    [Required] public Guid Id { get; set; }

    [Required] public string Content { get; set; } = null!;
    [Required] public DateTime Created { get; set; }

    public ICollection<ReportEntity> Reports { get; set; } = new HashSet<ReportEntity>();

    public static implicit operator Comment(CommentEntity entity) =>
        new()
        {
            Id = entity.Id,
            Content = entity.Content,
            Created = entity.Created,
        };

    public static implicit operator CommentEntity(Comment model) =>
        new()
        {
            Id = model.Id,
            Content = model.Content,
            Created = model.Created,
        };

}
