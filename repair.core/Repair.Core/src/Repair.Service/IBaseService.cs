using Repair.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Service;

public interface IBaseService<TEntity>
{
    #region 保存
    bool SaveChanges();
    Task<bool> SaveChangesAsync();
    #endregion

    #region 添加
    bool Add(TEntity entity, bool isSave = true);
    Task<bool> AddAsync(TEntity entity, bool isSave = true);
    bool AddRange(IEnumerable<TEntity> entitys, bool isSave = true);
    Task<bool> AddRangeAsync(IEnumerable<TEntity> entitys, bool isSave = true);
    #endregion

    #region 删除
    bool Delete(TEntity entity, bool isSave = true);
    bool Delete<Tkey>(Tkey key, bool isSave = true);
    Task<bool> DeleteAsync(TEntity entity, bool isSave = true);
    Task<bool> DeleteAsync<Tkey>(Tkey key, bool isSave = true);
    bool DeleteRange(IEnumerable<TEntity> entitys, bool isSave = true);
    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entitys, bool isSave = true);
    #endregion

    #region 更新
    bool Update(TEntity entity, bool isSave = true);
    Task<bool> UpdateAsync(TEntity entity, bool isSave = true);
    Task<bool> UpdateRangeAsync(List<TEntity> entitys, bool isSave = true);
    #endregion

    #region 查询
    IQueryable<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> lambda);
    IQueryable<TEntity> Query(bool isNotracking = true);
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true);

    /// <summary>
    /// 条件查询数据，获取Top行
    /// </summary>
    /// <param name="topCount">要获取的数据行数</param>
    /// <param name="lambda">条件表达式</param>
    /// <param name="isNotracking">是否无状态跟踪查询</param>
    /// <returns></returns>
    IQueryable<TEntity> QueryTop<TOrderKey>(int topCount, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null
        , bool asc = true
        , Expression<Func<TEntity, bool>> lambda = null, bool isNotracking = true);
    IQueryable<TEntity> QueryPaging<TOrderKey>(int pageIndex, int pageSize, out int total
        , Expression<Func<TEntity, bool>> lambdaWhere = null, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null
        , bool asc = true, bool isNotracking = true);
    Task<PageData<TEntity>> SelectListPagingAsync<TOrderKey>(int pageIndex, int pageSize
        , Expression<Func<TEntity, bool>> lambdaWhere = null, Expression<Func<TEntity, TOrderKey>> lambdaOrder = null
        , bool asc = true, bool isNotracking = true);
    TEntity Find<TKey>(TKey key);
    Task<TEntity> FindAsync<TKey>(TKey key);
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda, bool isNotracking = true);
    TEntity SingleOrDefault(Expression<Func<TEntity, bool>> lambda);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> lambda);
    bool Any(Expression<Func<TEntity, bool>> lambda);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> lambda);
    int Count();
    int Count(Expression<Func<TEntity, bool>> lambda);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<TEntity, bool>> lambda);
    TEntity Max();
    TResult Max<TResult>(Expression<Func<TEntity, TResult>> lambda);
    Task<TEntity> MaxAsync();
    Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> lambda);
    #endregion
}
