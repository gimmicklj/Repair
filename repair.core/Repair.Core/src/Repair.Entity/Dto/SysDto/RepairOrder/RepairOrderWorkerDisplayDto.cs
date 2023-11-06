using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.RepairOrder;

public class RepairOrderWorkerDisplayDto
{
    public int Id { get; set; }
    public string StudentNumber { get; set; }
    public int SpecificNumber { get; set; }
    public string Description { get; set; }
    public DateTime RepairTime { get; set; }
    public string PhoneNumber { get; set; }
    public string AreaName { get; set; }
    public string ImageUrls { get; set; }
    public bool IsRated { get; set; }
    public string OrderTypeDescription { get; set; }
    public string AuditSuggestion {  get; set; }
}
