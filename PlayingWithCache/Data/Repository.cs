using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithCache.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

            Entity = context.Set<TEntity>();
        }

        public DbSet<TEntity> Entity { get; }

        public IQueryable<TEntity> Entities => Entity.AsQueryable();

        public IQueryable<TEntity> EntitiesAsNoTracking => Entity.AsNoTracking().AsQueryable();

        public virtual async Task<TEntity> AddAsync(TEntity model, CancellationToken ct)
        {
            var t = await _context.AddAsync(model, ct);
            await _context.SaveChangesAsync(true, cancellationToken: ct);

            return t.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity model, CancellationToken ct)
        {
            _context.Attach(model);
            _context.Update(model);
            await _context.SaveChangesAsync(true, ct);

            return model;
        }

        public virtual async ValueTask<TEntity> FindAsync(object id, CancellationToken ct)
        {
            var t = await _context
                .Set<TEntity>()
                .FindAsync(new object[] { id }, ct);

            return t;
        }

        public virtual async Task<List<TEntity>> ListAsync(CancellationToken ct) =>
            await EntitiesAsNoTracking
                .ToListAsync(ct);
    }
}
