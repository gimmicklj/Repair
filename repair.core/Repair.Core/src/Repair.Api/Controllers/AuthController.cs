using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repair.Entity.Dto.SysDto.AppUser;
using Repair.Service.SysService;
using System.ComponentModel;

namespace Repair.Api.Controllers;

/// <summary>
/// 认控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("认证接口")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="authService"></param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    #region 注册登录
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="appUserInput"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] AppUserLoginDto appUserInput)
    {
        return Ok(await _authService.LoginAsync(appUserInput.UserName, appUserInput.Password));
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] AppUserAddDto dto)
    {
        return Ok(await _authService.RegisterAsync(dto));
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public IActionResult LoginOut()
    {
        return Ok(_authService.LoginOut());
    }
    #endregion
}
