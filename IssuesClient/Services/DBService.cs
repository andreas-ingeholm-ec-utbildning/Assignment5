using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IssuesClient.Services;

public abstract class DBService<T> where T : class
{

    //Used to fill test data
    public static int CachedListCount { get; protected set; }

    protected DataContext Context { get; } = new();

    public virtual async Task<T> CreateAsync(T entity)
    {
        _ = Context.Set<T>().Add(entity);
        _ = await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task Update(T entity)
    {
        _ = Context.Update(entity);
        _ = await Context.SaveChangesAsync();
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate) =>
        await Context.Set<T>().FirstOrDefaultAsync(predicate, CancellationToken.None);

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        var list = await Context.Set<T>().ToArrayAsync();
        CachedListCount = list.Length;
        return list;
    }

    public virtual async Task Remove(Expression<Func<T, bool>> predicate)
    {
        if (await Context.Set<T>().FirstOrDefaultAsync(predicate, CancellationToken.None) is T entity)
        {
            _ = Context.Remove(entity);
            _ = await Context.SaveChangesAsync();
        }
    }

}
