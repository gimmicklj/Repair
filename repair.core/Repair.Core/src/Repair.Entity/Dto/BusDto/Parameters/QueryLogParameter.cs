using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.BusDto.Parameters;

public class QueryLogParameter : QueryParameter
{
    public long OrderId { get; set; }
}
