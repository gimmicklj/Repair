using Repair.Entity.SysEntity;
using Repair.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Repository.BusRepository;

public class RepairOrderRepository : BaseRepository<RepairOrder>, IRepairOrderRepository
{
    public RepairOrderRepository(AppDbContext sqlDbcontext)
      : base(sqlDbcontext)
    {

    }
}
