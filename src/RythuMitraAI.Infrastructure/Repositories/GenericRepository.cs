using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RythuMitraAI.Infrastructure.Persistence;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }
}
