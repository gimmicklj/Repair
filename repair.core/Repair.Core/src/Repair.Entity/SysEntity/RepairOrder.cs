using Repair.Entity.SysEntity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repair.Entity.SysEntity
{
    public class RepairOrder : BaseEntity<long>
    {
        [Required]
        [MaxLength(50)]
        public string StudentNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string SpecificNumber { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime RepairTime { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(2000)]
        public string ImageUrls { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public bool IsRated { get; set; }

        [Required]
        public long AreaId { get; set; }
        public Area? Area { get; set; }

        public long? RepairWorkerId { get; set; }
        public AppUser? RepairWorker { get; set; }

        public Comment? Comment { get; set; }

        public List<AuditLog>? AuditLogs { get; set; }
    }
}
