using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Repair.Entity.SysEntity;

namespace Repair.EntityFrameworkCore
{
    public class EntityConfig
    {
        public class AppUserConfig : IEntityTypeConfiguration<AppUser>
        {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                builder.HasKey(user => user.Id);
                builder.Property(user => user.Id)
                       .ValueGeneratedOnAdd();
            }
        }

        public class AreaConfig : IEntityTypeConfiguration<Area>
        {
            public void Configure(EntityTypeBuilder<Area> builder)
            {
                builder.HasKey(area => area.Id);
                builder.Property(area => area.Id)
                       .ValueGeneratedOnAdd();
            }
        }

        public class AuditLogConfig : IEntityTypeConfiguration<AuditLog>
        {
            public void Configure(EntityTypeBuilder<AuditLog> builder)
            {
                builder.HasKey(log => log.Id);
                builder.Property(log => log.Id)
                       .ValueGeneratedOnAdd();
            }
        }


        public class CommentConfig : IEntityTypeConfiguration<Comment>
        {
            public void Configure(EntityTypeBuilder<Comment> builder)
            {
                builder.HasKey(order => order.Id);
                builder.Property(order => order.Id)
                       .ValueGeneratedOnAdd();
                builder.HasOne(comment => comment.RepairOrder).WithOne(order => order.Comment)
                    .HasForeignKey<Comment>(comment => comment.RepairOrderId);
            }
        }
        
        public class RepairOrderConfig : IEntityTypeConfiguration<RepairOrder>
        {
            public void Configure(EntityTypeBuilder<RepairOrder> builder)
            {
                builder.HasKey(order => order.Id);
                builder.Property(order => order.Id)
                       .ValueGeneratedOnAdd();
                builder.HasOne(order => order.Area).WithMany(area => area.RepairOrders)
                    .HasForeignKey(order => order.AreaId);
                builder.HasOne(order => order.RepairWorker).WithMany(user => user.RepairOrders)
                    .HasForeignKey(order => order.RepairWorkerId);
                builder.HasMany(order => order.AuditLogs).WithOne(area => area.RepairOrder);
            }
        }
    }
}
