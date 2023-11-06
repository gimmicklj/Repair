using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repair.Entity.SysEntity
{
    public class Area : BaseEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string AreaName { get; set; }

        public List<RepairOrder>? RepairOrders { get; set; }
    }
}
