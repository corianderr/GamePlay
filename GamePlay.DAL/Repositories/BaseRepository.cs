using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class {
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(ApplicationDbContext context) {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity) {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        await Context.SaveChangesAsync();

        return addedEntity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity) {
        var removedEntity = DbSet.Remove(entity).Entity;
        await Context.SaveChangesAsync();

        return removedEntity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties) {
        IQueryable<TEntity> query = DbSet;
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        if (predicate != null) return await query.Where(predicate).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties) {
        IQueryable<TEntity> query = DbSet;
        foreach (var includeProperty in includeProperties) {
            query = query.Include(includeProperty);
        }

        if (predicate != null) {
            return (await query.FirstOrDefaultAsync(predicate))!;
        }

        return (await query.FirstOrDefaultAsync())!;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity) {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }
}