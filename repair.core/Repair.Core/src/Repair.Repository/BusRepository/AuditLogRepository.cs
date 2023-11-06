using Repair.Entity.SysEntity;
using Repair.EntityFrameworkCore;

namespace Repair.Repository.BusRepository;

public class AuditLogRepository: BaseRepository<AuditLog>, IAuditLogRepository
{
    public AuditLogRepository(AppDbContext sqlDbcontext)
      : base(sqlDbcontext)
    {

    }
}
