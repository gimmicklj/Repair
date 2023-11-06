using Microsoft.EntityFrameworkCore;
using Repair.Entity.SysEntity;

namespace Repair.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<RepairOrder> RepairOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 在这里应用关系配置类
            modelBuilder.ApplyConfiguration(new EntityConfig.CommentConfig());
            modelBuilder.ApplyConfiguration(new EntityConfig.RepairOrderConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 全局关闭EF Core数据跟踪
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
