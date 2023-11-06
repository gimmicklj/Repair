using System.ComponentModel;

namespace Repair.Entity.SysEntity.Enum;

public enum Status
{
    [Description("待审核")]
    待审核 = 0,
    [Description("审核失败")]
    审核失败 = 1,
    [Description("待派单")]
    待派单 = 2,
    [Description("待处理")]
    待处理 = 3,
    [Description("被拒绝")]
    被拒绝 = 4,
    [Description("处理中")]
    处理中 = 5,
    [Description("已处理")]
    已处理 = 6,
    [Description("已完成")]
    已完成 = 7
}