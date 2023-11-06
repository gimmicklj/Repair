namespace Repair.Entity.Dto.BusDto.Parameters;

/// <summary>
/// 查询条件使用到的参数
/// </summary>
public class QueryParameter
{
    /// <summary>
    /// 第几页
    /// </summary>
    public int PageIndex { get; set; }
    /// <summary>
    /// 页面大小
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// 查询条件信息
    /// </summary>
    public string? Search { get; set; }
}

