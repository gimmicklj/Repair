using Repair.Entity.SysEntity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.RepairOrder;

public class DispatchOrderDTO
{
    public long OrderId { get; set; }
    public OrderType OrderType { get; set; }
    public long? RepairWorkerId { get; set; }
}