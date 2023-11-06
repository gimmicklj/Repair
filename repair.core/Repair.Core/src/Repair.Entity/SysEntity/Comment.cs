using System.ComponentModel.DataAnnotations;

namespace Repair.Entity.SysEntity
{
    public class Comment : BaseEntity<long>
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(500)]
        public string CommentText { get; set; }

        [Required]
        public long RepairOrderId { get; set; }

        public RepairOrder? RepairOrder { get; set; }
    }
}
