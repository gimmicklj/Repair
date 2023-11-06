using Repair.Entity.SysEntity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.AppUser;

public class AppUserAddDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public RoleType RoleId { get; set; }
}

