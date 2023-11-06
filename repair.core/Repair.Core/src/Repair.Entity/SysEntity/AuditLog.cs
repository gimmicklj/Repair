using System.ComponentModel.DataAnnotations;

namespace Repair.Entity.SysEntity
{
    public class AuditLog : BaseEntity<long>
    {
        [Required]
        [MaxLength(500)]
        public string Suggestion { get; set; }

        [Required]
        public long RepairOrderId { get; set; }

        public RepairOrder? RepairOrder { get; set; }
    }
}
