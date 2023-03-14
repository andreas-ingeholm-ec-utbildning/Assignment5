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

    public static int CachedCount { get; private set; }

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

        Context.Entry(entity.User).State = EntityState.Unchanged;
        foreach (var c in entity.Comments)
            Context.Entry(c).State = c.Id != comment.Id ? EntityState.Unchanged : EntityState.Added;

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
        var list = (await Context.Reports.Include(r => r.User).Include(r => r.User.User).Include(r => r.Comments).ToArrayAsync()).Select(r => (Report?)r).OfType<Report>();
        CachedCount = list.Count();
        return list;
    }

}
