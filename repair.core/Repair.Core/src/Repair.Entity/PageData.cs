namespace Repair.Entity;

public class PageData<TEntity>
{
    public List<TEntity>? List { get; set; }

    public int Total {  get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
