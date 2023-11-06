using Repair.Entity;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.Area;
using Repair.Entity.SysEntity;

namespace Repair.Service.SysService;

public interface IAreaService : IBaseService<Area>
{
    Task<ApiResult<AreaAddDto>> AddAsync(AreaAddDto dto);

    Task<ApiResult<bool>> DeleteAsync(long id);

    Task<ApiResult<AreaUpdateDto>> EditAsync(long id);

    Task<ApiResult<AreaUpdateDto>> UpdateAsync(AreaUpdateDto dto);

    Task<ApiResult<List<AreaSelectDto>>> GetAreaSelectAsync();

    Task<ApiResult<PageData<AreaSelectDto>>> GetAreaPageListAsync(QueryParameter queryParameter);
}
