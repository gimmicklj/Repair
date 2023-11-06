using Repair.Entity;
using Repair.Entity.Dto.BusDto.Echart;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.AppUser;
using Repair.Entity.SysEntity;

namespace Repair.Service.SysService;

public interface IAppUserService:IBaseService<AppUser>
{
    Task<AppUser> VerifyUserAsync(string userName, string passWord);

    Task<ApiResult<bool>> DeleteAsync(long id);

    Task<ApiResult<AppUserUpdateDto>> EditAppUserInfoAsync(long id);

    Task<ApiResult<AppUserUpdateDto>> UpdateAsync(AppUserUpdateDto dto);

    #region 查询
    Task<ApiResult<AppUserDto>> GetAppUserInfoAsync();

    Task<ApiResult<List<RepairWorkerSelectDto>>> GetRepairWorkerSelectAsync();

    Task<ApiResult<List<AppUserDto>>> GetPagedListAppUser();

    Task<ApiResult<List<EmployeeEvaluationDto>>> GetEmployeeEvaluation();
    #endregion
}
