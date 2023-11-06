using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.AuditLog;

public class AuditLogUpdateDto
{
    public int Id {  get; set; }

    public string Suggestion { get; set; }
}
