using Repair.Entity;
using Repair.Entity.Dto.BusDto.Auth;
using Repair.Entity.Dto.SysDto.AppUser;

namespace Repair.Service.SysService;

public interface IAuthService
{
    Task<ApiResult<AuthDto>> LoginAsync(string username, string password);

    Task<ApiResult<AppUserAddDto>> RegisterAsync(AppUserAddDto dto);

    ApiResult<bool> LoginOut();
}
