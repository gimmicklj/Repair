namespace Repair.Core.Helper.Jwt;

public class TokenPayload
{
    /// <summary>
    /// 编号
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 角色
    /// </summary>
    public string? Role { get; set; }

}