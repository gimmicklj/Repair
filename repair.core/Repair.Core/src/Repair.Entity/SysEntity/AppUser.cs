using Repair.Entity.SysEntity.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repair.Entity.SysEntity
{
    public class AppUser : BaseEntity<long>
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }

        [Required]
        public RoleType RoleId { get; set; }

        public List<RepairOrder>? RepairOrders { get; set; }
    }
}
