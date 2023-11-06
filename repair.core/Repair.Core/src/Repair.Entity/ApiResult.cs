namespace Repair.Entity;

public class ApiResult<T>
{
    /// <summary>
    /// 状态码，默认200
    /// </summary>
    public int Code { get; set; } = 200;

    /// <summary>
    /// 消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 通用数据
    /// </summary>
    public T? Data { get; set; }
}
