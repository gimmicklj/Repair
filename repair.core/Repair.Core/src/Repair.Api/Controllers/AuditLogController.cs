using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.AuditLog;
using Repair.Service.SysService;
using System.ComponentModel;

namespace Repair.Api.Controllers;
/// <summary>
/// 审核日志控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("审核日志接口")]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="auditLogService"></param>
    public AuditLogController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    /// <summary>
    /// 添加审核日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] AuditLogAddDto dto)
    {
        return Ok(await _auditLogService.AddAsync(dto));
    }

    /// <summary>
    /// 删除审核日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(await _auditLogService.DeleteAsync(id));
    }

    /// <summary>
    /// 编辑审核日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditAsync(long id)
    {
        return Ok(await _auditLogService.EditAsync(id));
    }

    /// <summary>
    /// 修改审核日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromBody] AuditLogUpdateDto dto)
    {
        return Ok(await _auditLogService.UpdateAsync(dto));
    }


    /// <summary>
    /// 分页展示订单的审核日志
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAuditLogPagedListAsync(long OrderId)
    {
        return Ok(await _auditLogService.GetAuditLogPagedListAsync(OrderId));
    }
}
