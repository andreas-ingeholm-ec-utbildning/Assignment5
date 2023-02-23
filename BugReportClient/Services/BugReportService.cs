using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugReportClient.Models;
using BugReportClient.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static BugReportClient.Services.DBService;

namespace BugReportClient.Services;

public static class BugReportService
{

    #region Get

    /// <summary>Used by AddBugReportPopup for filling test data.</summary>
    public static int CachedListCount { get; private set; }

    public static async Task<IEnumerable<BugReport>> GetAllAsync()
    {
        var list = (await Context.BugReports.Include(r => r.User).ToArrayAsync()).Select(ToModel);
        CachedListCount = list.Count();
        return list;
    }

    public static async Task<BugReport?> GetAsync(Guid id) =>
      (await GetEntityAsync(id))?.ToModel();

    public static async Task<BugReportEntity?> GetEntityAsync(BugReport report) =>
        (await Context.BugReports.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == report.Id));

    public static async Task<BugReportEntity?> GetEntityAsync(Guid id) =>
        (await Context.BugReports.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id));

    #endregion
    #region Save

    public static async Task SaveAsync(BugReport model)
    {

        var entity = model.ToEntity();
        entity.User =
            await UserService.GetEntityAsync(model.User) is UserEntity user
            ? user
            : throw new InvalidOperationException("Cannot retrieve user (" + model.User.EmailAddress ?? model.User.Id + ")");

        _ = Context.Add(entity);
        _ = await Context.SaveChangesAsync();

    }

    #endregion
    #region Delete

    public static async Task DeleteAsync(BugReport report)
    {

        if (await GetEntityAsync(report) is BugReportEntity entity)
        {
            _ = Context.Remove(entity);
            _ = await Context.SaveChangesAsync();
        }

    }

    #endregion
    #region To/from entity/model

    public static BugReportEntity ToEntity(this BugReport model) =>
        new() { Id = model.Id, Title = model.Title, Content = model.Content };

    public static BugReport ToModel(this BugReportEntity entity) =>
        new() { Id = entity.Id, Title = entity.Title, Content = entity.Content, User = entity.User.ToModel()! };

    #endregion

}
