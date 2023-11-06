using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.Area;
using Repair.Service.SysService;
using System.ComponentModel;

namespace Repair.Api.Controllers;

/// <summary>
/// 区域控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("区域接口")]
public class AreaController : ControllerBase
{
    private readonly IAreaService _areaService;

    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="areaService"></param>
    public AreaController(IAreaService areaService)
    {
        _areaService = areaService;
    }

    /// <summary>
    /// 添加区域
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] AreaAddDto dto)
    {
         return Ok(await _areaService.AddAsync(dto));
    }

    /// <summary>
    /// 删除区域
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(await _areaService.DeleteAsync(id));
    }

    /// <summary>
    /// 编辑区域
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditAsync(long id)
    {
        return Ok(await _areaService.EditAsync(id));
    }

    /// <summary>
    /// 修改区域
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromBody] AreaUpdateDto dto)
    {
        return Ok(await _areaService.UpdateAsync(dto));
    }

    #region 查询
    /// <summary>
    /// 获取区域选择器
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetAreaSelectAsync()
    {
        return Ok( await _areaService.GetAreaSelectAsync());
    }

    /// <summary>
    /// 分页获取地区信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetAreaPageListAsync([FromQuery] QueryParameter query)
    {
        return Ok(await _areaService.GetAreaPageListAsync(query));
    }
    #endregion
}
