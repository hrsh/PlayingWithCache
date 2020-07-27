using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithCache.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        IQueryable<TEntity> EntitiesAsNoTracking { get; }
        DbSet<TEntity> Entity { get; }

        Task<TEntity> AddAsync(TEntity model, CancellationToken ct);
        ValueTask<TEntity> FindAsync(object id, CancellationToken ct);
        Task<List<TEntity>> ListAsync(CancellationToken ct);
        Task<TEntity> UpdateAsync(TEntity model, CancellationToken ct);
    }
}