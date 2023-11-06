using Microsoft.EntityFrameworkCore;

namespace Repair.EntityFrameworkCore.Collections;

public class PageHelper<T> : List<T>
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex { get; set; }
    //总页面数
    public int TotalPages { get; set; }

    public PageHelper(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);
        this.AddRange(items);
    }
    /// <summary>
    /// 判断是否有上一页
    /// </summary>
    public bool HasPreViousPage => (PageIndex > 1);
    /// <summary>
    /// 判断是否有下一页
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;
    /// <summary>
    /// 创建分页
    /// </summary>
    /// <param name="source">实体信息</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">当前页数据条数</param>
    /// <returns></returns>
    public static async Task<PageHelper<T>> ToPageListAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        //返回实体总条数
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageHelper<T>(items, count, pageIndex, pageSize);

    }
}

