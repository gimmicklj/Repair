using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class BaseEntity<TKey>
{
    // 主键，不允许为空
    [Required]
    public TKey Id { get; set; }

    // 排序，可以为空
    [AllowNull]
    public int? Sort { get; set; }

    // 数据创建时间，不允许为空
    [Required]
    public DateTime CreateTime { get; set; }

    // 数据修改时间，不允许为空
    [Required]
    public DateTime UpdateTime { get; set; }

    // 创建人id，不允许为空
    public long? CreatorId { get; set; }

    [Required]
    // 创建人Name，不允许为空
    public string? CreatorName { get; set; }

    // 是否删除，可以为空
    public bool? isDelete { get; set; }
}
