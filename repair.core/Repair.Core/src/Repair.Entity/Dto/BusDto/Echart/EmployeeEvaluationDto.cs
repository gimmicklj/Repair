using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair.Entity.Dto.BusDto.Echart;

public class EmployeeEvaluationDto
{
    public long EmployeeId { get; set; }
    public string Name { get; set; }
    public int GoodCount { get; set; }
    public int AverageCount { get; set; }
    public int PoorCount { get; set; }
}
