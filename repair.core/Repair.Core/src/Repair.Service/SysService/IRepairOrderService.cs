using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.RepairOrder;

namespace Repair.Service.SysService;

public interface IRepairOrderService :IBaseService<RepairOrder>
{
    Task<ApiResult<RepairOrderAddDto>> AddAsync(RepairOrderAddDto dto);

    Task<ApiResult<bool>> DeleteAsync(long id);

    Task<ApiResult<RepairOrderUpdateDto>> EditAsync(long id);

    Task<ApiResult<RepairOrderUpdateDto>> UpdateAsync(RepairOrderUpdateDto dto);

    Task<ApiResult<bool>> ApproveOrderAsync(long id);

    Task<ApiResult<bool>> DispatchOrderAsync(DispatchOrderDTO dto);

    Task<ApiResult<bool>> RefuseOrderAsync(long orderId);

    Task<ApiResult<bool>> TakeOrderAsync(long orderId);

    Task<ApiResult<bool>> FinishOrderAsync(long orderId);
    
    #region 查询
    Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetTobeAuditOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetTobeFinishedOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetNotRatingOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetRatedOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderWorkerDisplayDto>>> GetPendingOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderWorkerDisplayDto>>> GetUnTakeOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderWorkerDisplayDto>>> GetRatedByUserOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderAssignmentDto>>> GetTobeReviewedOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderAssignmentDto>>> GetUnTakeByAdminOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderAdminDisplayDto>>> GetAllUnFinishedOrderPagedListAsync();

    Task<ApiResult<List<RepairOrderAdminDisplayDto>>> GetAllFinishedOrderPagedListAsync();
    #endregion
}
