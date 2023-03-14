using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IssuesClient.Services;

namespace IssuesClient.Models.Entities;

public enum ReportStatus
{
    Pending,
    InProgress,
    Completed
}

public class ReportEntity
{

    [Required] public Guid Id { get; set; }

    [Required] public string Title { get; set; } = null!;
    [Required] public ReportStatus Status { get; set; }
    [Required] public DateTime Created { get; set; }

    [Required] public UserProfileEntity User { get; set; } = null!;
    [Required] public List<CommentEntity> Comments { get; set; } = null!;

    public static implicit operator Report?(ReportEntity? entity) =>
        entity is null
        ? null
        : new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Status = entity.Status,
            Created = entity.Created,
            User = entity.User!,
            Comments = entity.Comments.Select(c => (Comment)c).ToList(),
        };

    public static implicit operator ReportEntity(Report model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Status = model.Status,
            Created = model.Created,
            User = DBService.Context.Profiles.Entry(model.User).Entity,
            Comments = model.Comments.Select(c => (CommentEntity)c).ToList(),
        };

}
