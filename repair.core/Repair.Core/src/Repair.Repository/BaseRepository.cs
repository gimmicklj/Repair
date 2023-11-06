using Microsoft.EntityFrameworkCore;
using Repair.Entity;
using Repair.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repair.Repository;

/// <summary>
/// 泛型仓储基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{

    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _entitys;

    public BaseRepository(AppDbContext sqlServerDbContext)
    {
        _context = sqlServerDbContext;
        _entitys = _context.Set<TEntity>();
    }

    public AppDbContext GetDbContext()
    {
        return _context;
    }

    #region 保存

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion

    #region 添加
    public bool Add(TEntity entity, bool isSave = true)
    {
        _entitys.Add(entity);
        if (isSave)
            return SaveChanges();
        return false;

    }

    public async Task<bool> AddAsync(TEntity entity, bool isSave = true)
    {
        await _entitys.AddAsync(entity);
        if (isSave)
            return await SaveChangesAsync();
        return false;

    }

    public bool AddRange(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        _entitys.AddRange(entitys);
        if (isSave)
            return SaveChanges();
        return false;

    }

    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        await _entitys.AddRangeAsync(entitys);
        if (isSave)
            return await SaveChangesAsync();
        return false;

    }

    #endregion

    #region 删除
    public bool Delete(TEntity entity, bool isSave = true)
    {
        _entitys.Remove(entity);
        if (isSave)
            return SaveChanges();
        return false;

    }

    public bool Delete<Tkey>(Tkey key, bool isSave = true)
    {
        TEntity entity = _entitys.Find(key);
        _entitys.Remove(entity);
        if (isSave)
            return SaveChanges();
        return false;
    }

    public async Task<bool> DeleteAsync(TEntity entity, bool isSave = true)
    {
        Delete(entity, false);
        if (isSave)
            return await SaveChangesAsync();
        return false;
    }

    public async Task<bool> DeleteAsync<Tkey>(Tkey key, bool isSave = true)
    {
        Delete(key, false);
        if (isSave)
            return await SaveChangesAsync();
        return false;
    }

    public bool DeleteRange(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        _entitys.RemoveRange(entitys);
        if (isSave)
            return SaveChanges();
        return false;
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        DeleteRange(entitys, false);
        if (isSave)
        {
            return await SaveChangesAsync();
        }
        return false;
    }

    #endregion

    #region 更新
    public bool Update(TEntity entity, bool isSave = true)
    {
        _entitys.Update(entity);
        if (isSave)
            return SaveChanges();
        return false;
    }

    public async Task<bool> UpdateAsync(TEntity entity, bool isSave = true)
    {
        Update(entity, false);
        if (isSave)
            return await SaveChangesAsync();
        return false;
    }

    public async Task<bool> UpdateRangeAsync(List<TEntity> entitys, bool isSave = true)
    {
        _entitys.UpdateRange(entitys);
        if (isSave)
            return await SaveChangesAsync();
        return false;

    }

    #endregion

    #region 查询
    public IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> lambda)
    {
        return _entitys.Include(lambda);
    }

    public IQueryable<TEntity> Query(bool isNotracking = true)
    {
        return isNotracking ? _entitys.AsNoTracking() : _entitys.AsQueryable();
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true)
    {
        return isNotracking ? _entitys.AsNoTracking().Where(lambda) : _entitys.Where(lambda);
    }

    public IQueryable<TEntity> QueryTop<TOrderKey>(int topCount, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null
        , bool asc = true
        , Expression<Func<TEntity, bool>> lambda = null, bool isNotracking = true)
    {
        var query = _entitys.AsQueryable();
        if (isNotracking)
        {
            query = query.AsNoTracking();
        }

        if (lambda != null)
        {
            query = query.Where(lambda);
        }

        if (lambdaOrder != null)
        {
            query = asc ? query.OrderBy(lambdaOrder) : query.OrderByDescending(lambdaOrder);
        }

        return query.Take(topCount);
    }

    public IQueryable<TEntity> QueryPaging<TOrderKey>(int pageIndex, int pageSize, out int total
        , Expression<Func<TEntity, bool>> lambdaWhere, Expression<Func<TEntity, TOrderKey>> lambdaOrder
        , bool asc = true, bool isNotracking = true)
    {
        var query = isNotracking ? _entitys.AsNoTracking() : _entitys.AsQueryable();

        if (lambdaWhere != null)
        {
            query = query.Where(lambdaWhere);
        }

        total = query.Count();
        if (lambdaOrder != null)
        {
            query = asc ? query.OrderBy(lambdaOrder) : query.OrderByDescending(lambdaOrder);
        }

        return query.Skip((pageIndex + 1) * pageSize).Take(pageSize);
    }

    public async Task<PageData<TEntity>> SelectListPagingAsync<TOrderKey>(int pageIndex, int pageSize
        , Expression<Func<TEntity, bool>> lambdaWhere = null, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null
        , bool asc = true, bool isNotracking = true)
    {
        var query = isNotracking ? _entitys.AsNoTracking() : _entitys.AsQueryable();

        if (lambdaWhere != null)
        {
            query = query.Where(lambdaWhere);
        }

        int total = query.Count();

        if (lambdaOrder != null)
        {
            query = asc ? query.OrderBy(lambdaOrder) : query.OrderByDescending(lambdaOrder);
        }

        return new PageData<TEntity> { List = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(), Total = total };
    }

    public TEntity Find<TKey>(TKey key)
    {
        return _entitys.Find(key);
    }

    public async Task<TEntity> FindAsync<TKey>(TKey key)
    {
        return await _entitys.FindAsync(key);
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true)
    {
        return isNotracking ? _entitys.AsNoTracking().FirstOrDefault(lambda) : _entitys.FirstOrDefault(lambda);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true)
    {
        return isNotracking ? await _entitys.AsNoTracking().FirstOrDefaultAsync(lambda) : await _entitys.FirstOrDefaultAsync(lambda);
    }

    public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> lambda)
    {
        return _entitys.AsNoTracking().SingleOrDefault(lambda);
    }

    public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> lambda)
    {
        return _entitys.AsNoTracking().SingleOrDefaultAsync(lambda);
    }

    public bool Any(Expression<Func<TEntity, bool>> lambda)
    {
        return _entitys.Any(lambda);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> lambda)
    {
        return await _entitys.AnyAsync(lambda);
    }

    public int Count()
    {
        return _entitys.Count();
    }

    public int Count(Expression<Func<TEntity, bool>> lambda)
    {
        return _entitys.Count(lambda);
    }

    public async Task<int> CountAsync()
    {
        return await _entitys.CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> lambda)
    {
        return await _entitys.CountAsync(lambda);
    }

    public TEntity Max()
    {
        return _entitys.Max();
    }

    public TResult Max<TResult>(Expression<Func<TEntity, TResult>> lambda)
    {
        return _entitys.Max(lambda);
    }

    public async Task<TEntity> MaxAsync()
    {
        return await _entitys.MaxAsync();
    }

    public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> lambda)
    {
        return await _entitys.MaxAsync(lambda);
    }
    #endregion
}