using Repair.Entity;
using Repair.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Repair.Service;
public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    protected readonly IBaseRepository<TEntity> _repository;
    //通过子类构造函数注入仓储实现类，传进来
    public BaseService(IBaseRepository<TEntity> repository)
    {
        this._repository = repository;
    }

    #region 保存

    public bool SaveChanges()
    {
        return _repository.SaveChanges();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _repository.SaveChangesAsync();
    }
    #endregion

    public bool Add(TEntity entity, bool isSave = true)
    {
        return _repository.Add(entity, isSave);
    }

    public async Task<bool> AddAsync(TEntity entity, bool isSave = true)
    {
        return await _repository.AddAsync(entity, isSave);
    }

    public bool AddRange(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        return _repository.AddRange(entitys, isSave);
    }

    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        return await _repository.AddRangeAsync(entitys, isSave);
    }

    public bool Delete(TEntity entity, bool isSave = true)
    {
        return _repository.Delete(entity, isSave);
    }

    public bool Delete<Tkey>(Tkey key, bool isSave = true)
    {
        return _repository.Delete(key, isSave);
    }

    public async Task<bool> DeleteAsync(TEntity entity, bool isSave = true)
    {
        return await _repository.DeleteAsync(entity, isSave);
    }

    public async Task<bool> DeleteAsync<Tkey>(Tkey key, bool isSave = true)
    {
        return await _repository.DeleteAsync(key, isSave);
    }

    public bool DeleteRange(IEnumerable<TEntity> entitys, bool isSave = true)
    {
        return _repository.DeleteRange(entitys, isSave);
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitys, bool isSave = true)
    {

        return await _repository.DeleteRangeAsync(entitys, isSave);
    }

    public bool Update(TEntity entity, bool isSave = true)
    {
        return _repository.Update(entity, isSave);
    }

    public async Task<bool> UpdateAsync(TEntity entity, bool isSave = true)
    {
        return await _repository.UpdateAsync(entity, isSave);
    }

    public async Task<bool> UpdateRangeAsync(List<TEntity> entitys, bool isSave = true)
    {
        return await _repository.UpdateRangeAsync(entitys, isSave);
    }

    public IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> lambda)
    {
        return _repository.Include(lambda);
    }

    public IQueryable<TEntity> Query(bool isNotracking = true)
    {

        return _repository.Query(isNotracking);
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true)
    {

        return _repository.Query(lambda, isNotracking);
    }

    public IQueryable<TEntity> QueryTop<TOrderKey>(int topCount, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null
        , bool asc = true
        , Expression<Func<TEntity, bool>> lambda = null, bool isNotracking = true)
    {
        return _repository.QueryTop(topCount, lambdaOrder, asc, lambda, isNotracking);
    }

    public IQueryable<TEntity> QueryPaging<TOrderKey>(int pageIndex, int pageSize, out int total, Expression<Func<TEntity, bool>> lambdaWhere = null, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null, bool asc = true, bool isNotracking = true)
    {
        return _repository.QueryPaging(pageIndex, pageSize, out total, lambdaWhere, lambdaOrder, asc, isNotracking);
    }

    public Task<PageData<TEntity>> SelectListPagingAsync<TOrderKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> lambdaWhere = null, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null, bool asc = true, bool isNotracking = true)
    {

        return _repository.SelectListPagingAsync(pageIndex, pageSize, lambdaWhere, lambdaOrder, asc, isNotracking);
    }

    public TEntity Find<TKey>(TKey key)
    {
        return _repository.Find(key);
    }

    public async Task<TEntity> FindAsync<TKey>(TKey key)
    {
        return await _repository.FindAsync(key);
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true)
    {
        return _repository.FirstOrDefault(lambda, isNotracking);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true)
    {
        return await _repository.FirstOrDefaultAsync(lambda, isNotracking);
    }

    public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> lambda)
    {
        return _repository.SingleOrDefault(lambda);
    }

    public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> lambda)
    {
        return await _repository.SingleOrDefaultAsync(lambda);
    }

    public bool Any(Expression<Func<TEntity, bool>> lambda)
    {
        return _repository.Any(lambda);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> lambda)
    {
        return await _repository.AnyAsync(lambda);
    }

    public int Count()
    {
        return _repository.Count();
    }

    public int Count(Expression<Func<TEntity, bool>> lambda)
    {
        return _repository.Count(lambda);
    }

    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> lambda)
    {
        return await _repository.CountAsync(lambda);
    }

    public TEntity Max()
    {

        return _repository.Max();
    }

    public TResult Max<TResult>(Expression<Func<TEntity, TResult>> lambda)
    {
        return _repository.Max(lambda);
    }

    public async Task<TEntity> MaxAsync()
    {
        return await _repository.MaxAsync();
    }

    public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> lambda)
    {
        return await _repository.MaxAsync(lambda);
    }
}
