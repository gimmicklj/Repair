using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.AuditLog;

public class AuditLogDto
{
    public long Id { get; set; }

    public string Suggestion { get; set; }

    public string SpecificNumber { get; set; }

    public DateTime CreateTime { get; set; }

    public string CreatorName { get; set; }
}
