using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.SysEntity.Enum;

public enum OrderType
{
    [Description("普通订单")]
    普通订单 = 0,
    [Description("加急订单")]
    加急订单 = 1,
}
