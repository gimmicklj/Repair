using Repair.Entity.SysEntity;
using Repair.EntityFrameworkCore;


namespace Repair.Repository.BusRepository;

public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
{
    public AppUserRepository(AppDbContext sqlDbcontext)
      : base(sqlDbcontext)
    {

    }
}
