using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.SysDto.AppUser;

public class AppUserDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
