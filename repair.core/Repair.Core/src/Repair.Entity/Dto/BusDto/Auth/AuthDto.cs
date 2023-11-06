using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.BusDto.Auth;

public class AuthDto
{
    /// <summary>
    /// 验证结果
    /// </summary>
    public bool VerifyResult { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// token字符串
    /// </summary>
    public string? Token { get; set; }
}
