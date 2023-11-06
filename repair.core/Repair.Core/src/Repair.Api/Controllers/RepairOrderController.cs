using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.RepairOrder;
using Repair.Service.SysService;
using System.ComponentModel;

namespace Repair.Api.Controllers;

/// <summary>
/// 报销单控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("报修单接口")]
public class RepairOrderController : ControllerBase
{
    private readonly IRepairOrderService _repairOrderService;

    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="repairOrderService"></param>
    public RepairOrderController(IRepairOrderService repairOrderService)
    {
        _repairOrderService = repairOrderService;
    }

    /// <summary>
    /// 添加订单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] RepairOrderAddDto dto)
    {
        return Ok(await _repairOrderService.AddAsync(dto));
    }

    /// <summary>
    /// 删除订单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(await _repairOrderService.DeleteAsync(id));
    }

    /// <summary>
    /// 编辑订单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditAsync(long id)
    {
        return Ok(await _repairOrderService.EditAsync(id));
    }

    /// <summary>
    /// 修改订单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromBody] RepairOrderUpdateDto dto)
    {
        return Ok(await _repairOrderService.UpdateAsync(dto));
    }

    #region 维修工
    /// <summary>
    /// 维修工拒接
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> RefuseAsync(long orderId)
    {
        return Ok(await _repairOrderService.RefuseOrderAsync(orderId));
    }

    /// <summary>
    /// 维修工接单
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> TakeAsync(long orderId)
    {
        return Ok(await _repairOrderService.TakeOrderAsync(orderId));
    }

    /// <summary>
    /// 维修工结单
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> FinishAsync(long orderId)
    {
        return Ok(await _repairOrderService.FinishOrderAsync(orderId));
    }
    #endregion

    #region 管理员
    /// <summary>
    /// 审核通过订单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ApproveAsync(long id)
    {
        return Ok(await _repairOrderService.ApproveOrderAsync(id));
    }

    /// <summary>
    /// 管理员派单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DispatchAsync([FromBody] DispatchOrderDTO dto)
    {
        return Ok(await _repairOrderService.DispatchOrderAsync(dto));
    }
    #endregion

    #region 查询
    /// <summary>
    /// 分页获取用户的待审核的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTobeAuditOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetTobeAuditOrderPagedListAsync());
    }

    /// <summary>
    /// 分页获取用户的待处理的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTobeFinishedOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetTobeFinishedOrderPagedListAsync());
    }


    /// <summary>
    /// 分页获取用户的未评价的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetNotRatingOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetNotRatingOrderPagedListAsync());
    }

    /// <summary>
    /// 分页获取用户已评价的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRatedOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetRatedOrderPagedListAsync());
    }

    /// <summary>
    /// 分页获取维修人员的待处理的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUnTakeOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetUnTakeOrderPagedListAsync());
    }

    /// <summary>
    /// 获取维修人员的处理中的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPendingOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetPendingOrderPagedListAsync());
    }
 
    /// <summary>
    /// 分页获取维修人员的完成了的订单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRatedByUserOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetRatedByUserOrderPagedListAsync());
    }

    /// <summary>
    /// 分页查询待审核的维修单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTobeReviewedOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetTobeReviewedOrderPagedListAsync());
    }

    /// <summary>
    /// 分页查询待派单的维修单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUnTakeByAdminOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetUnTakeByAdminOrderPagedListAsync());
    }

    /// <summary>
    /// 分页查询所有未完成维修单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllUnFinishedOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetAllUnFinishedOrderPagedListAsync());
    }

    /// <summary>
    /// 分页查询所有完成维修单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllFinishedOrderPagedListAsync()
    {
        return Ok(await _repairOrderService.GetAllFinishedOrderPagedListAsync());
    }
    #endregion

}
