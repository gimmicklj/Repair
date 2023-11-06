using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.AppUser;
using Repair.Service.SysService;
using System.ComponentModel;

namespace Repair.Api.Controllers;

/// <summary>
/// 用户控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("用户接口")]
public class AppUserController : ControllerBase
{
    private readonly IAppUserService _appUserService;

    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="appUserService"></param>
    public AppUserController(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditAsync(long id)
    {
        return Ok(await _appUserService.EditAppUserInfoAsync(id));
    }

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(AppUserUpdateDto dto)
    {
        return Ok(await _appUserService.UpdateAsync(dto));
    }

    /// <summary>
    /// 删除用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(await _appUserService.DeleteAsync(id));
    }

    #region 查询
    /// <summary>
    /// 获取个人信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserInfoAsync()
    {
        return Ok(await _appUserService.GetAppUserInfoAsync());
    }

    /// <summary>
    /// 获取维修工选择器
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRepairWorkerSelectAsync()
    {
        return Ok(await _appUserService.GetRepairWorkerSelectAsync());
    }

    /// <summary>
    /// 分页获取所有用户信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPagedListAppUser()
    {
        return Ok(await _appUserService.GetPagedListAppUser());
    }

    /// <summary>
    /// 获取维修工订单情况
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEmployeeEvaluationCounts()
    {
        return Ok(await _appUserService.GetEmployeeEvaluation());
    }
    #endregion
}
