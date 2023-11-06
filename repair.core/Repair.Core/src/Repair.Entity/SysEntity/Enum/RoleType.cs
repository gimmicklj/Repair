using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.SysEntity.Enum;

public enum RoleType
{
    [Description("管理员")]
    管理员 = 0,
    [Description("维修人员")]
    维修人员 = 1,
    [Description("普通用户")]
    普通用户 = 2
}