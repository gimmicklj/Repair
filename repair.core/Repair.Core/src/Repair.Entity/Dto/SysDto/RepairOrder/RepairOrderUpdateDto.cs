using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.RepairOrder;

public class RepairOrderUpdateDto
{
    public long Id { get; set; }
    public string StudentNumber { get; set; }
    public string SpecificNumber { get; set; }
    public string Description { get; set; }
    public DateTime RepairTime { get; set; }
    public string PhoneNumber { get; set; }
    public long AreaId { get; set; }
    public string ImageUrls { get; set; }
}
