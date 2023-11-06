using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.AuditLog;

namespace Repair.Service.SysService;

public interface IAuditLogService : IBaseService<AuditLog>
{
    Task<ApiResult<AuditLogAddDto>> AddAsync(AuditLogAddDto dto);

    Task<ApiResult<bool>> DeleteAsync(long id);

    Task<ApiResult<AuditLogUpdateDto>> EditAsync(long id);

    Task<ApiResult<AuditLogUpdateDto>> UpdateAsync(AuditLogUpdateDto dto);

    Task<ApiResult<List<AuditLogDto>>> GetAuditLogPagedListAsync(long OrderId);
}
