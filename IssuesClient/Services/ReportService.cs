using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuesClient.Models;
using IssuesClient.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static IssuesClient.Services.DBService;

namespace IssuesClient.Services;

static class ReportService
{

    /// <summary>Used for filling debug data.</summary>
    public static int CachedCount { get; private set; }

    static ReportService()
    {
        Context.ChangeTracker.DetectedEntityChanges += (s, e) =>
        {

            //Comments are not automatically deleted from db, this appears to solve that
            if (e.Entry.State == EntityState.Deleted && e.Entry.Entity is ReportEntity report)
                foreach (var comment in report.Comments)
                    _ = Context.Comments.Remove(comment);

        };
    }

    public static async Task CreateAsync(Report report)
    {

        if (report.Comments?.Count == 0)
            throw new InvalidOperationException("Each report must have at least one comment.");

        var entity = (ReportEntity)report;

        Context.ChangeTracker.Clear();

        _ = await Context.Reports.AddAsync(entity);
        Context.Entry(entity.User).State = EntityState.Unchanged;
        Context.Entry(entity.User.User).State = EntityState.Unchanged;

        _ = await Context.SaveChangesAsync();

    }

    public static async Task AddCommentAsync(Report report, Comment comment)
    {

        var entity = Context.Set<ReportEntity>().Local.First(r => r.Id == report.Id);
        entity.Comments.Add(comment);
        _ = Context.Reports.Update(entity);

        //User is always detected as changed, for some reason, we'll need to unset that
        Context.Entry(entity.User).State = EntityState.Unchanged;

        //Existing comments are also detected as changed, we'll unset those too
        //New comment is also not detected as added for some reason
        foreach (var c in entity.Comments)
            Context.Entry(c).State = c.Id != comment.Id ? EntityState.Unchanged : EntityState.Added;

        _ = await Context.SaveChangesAsync();

    }

    public static async Task SetStatusAsync(Report report, ReportStatus status)
    {

        var entity = Context.Set<ReportEntity>().Local.First(r => r.Id == report.Id);
        entity.Status = status;

        _ = Context.Reports.Update(entity);
        _ = await Context.SaveChangesAsync();

    }

    public static async Task RemoveAsync(Report report)
    {
        if (await Context.Reports.FirstOrDefaultAsync(r => r.Id == report.Id) is ReportEntity entity)
        {
            _ = Context.Reports.Remove(entity);
            _ = await Context.SaveChangesAsync();
        }
    }

    public static async Task<Report?> GetAsync(Guid id) =>
       await Context.Reports.Include(r => r.User).Include(r => r.User.User).FirstOrDefaultAsync(p => p.Id == id);

    public static async Task<IEnumerable<Report>> GetAllAsync()
    {

        var list =
            (await Context.Reports.Include(r => r.User).Include(r => r.User.User).Include(r => r.Comments).ToArrayAsync()).
            Select(r => (Report?)r).
            OfType<Report>().
            OrderBy(r => r.Created);

        CachedCount = list.Count();
        return list;

    }

}
