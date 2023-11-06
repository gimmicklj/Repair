using Repair.Entity.SysEntity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.AppUser;

public class AppUserUpdateDto
{
    public long Id { get; set; }
    public string ?UserName { get; set; }
    public string ?Name { get; set; }
    public string ?Email { get; set; }
}
