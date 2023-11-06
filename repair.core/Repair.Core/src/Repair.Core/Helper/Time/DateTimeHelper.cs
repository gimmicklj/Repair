namespace Repair.Core.Helper.Time;

/// <summary>
/// 日期时间帮助类
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns></returns>
    public static DateTime GetThisDateTime()
    {
        return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
