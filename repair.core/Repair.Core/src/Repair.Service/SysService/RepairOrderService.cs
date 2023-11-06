using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Repository.BusRepository;
using AutoMapper;
using Repair.Core.Helper.Jwt;
using Microsoft.AspNetCore.Http;
using Repair.Entity.SysEntity.Enum;
using Microsoft.EntityFrameworkCore;
using Repair.Core.Extensions.String;
using Repair.EntityFrameworkCore;
using Repair.Core.Extensions.Enum;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.RepairOrder;
using Repair.EntityFrameworkCore.Collections;

namespace Repair.Service.SysService;

public class RepairOrderService : BaseService<RepairOrder>, IRepairOrderService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public RepairOrderService(IRepairOrderRepository repairOrderRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : base(repairOrderRepository)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _context = repairOrderRepository.GetDbContext();
    }

    /// <summary>
    /// 添加维修单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<RepairOrderAddDto>> AddAsync(RepairOrderAddDto dto)
    {
            var order = _mapper.Map<RepairOrder>(dto);
            order.Status = Status.待审核;
            order.OrderType = OrderType.普通订单;
            order.CreateTime = DateTime.Now;
            order.UpdateTime = DateTime.Now;
            order.IsRated = false;
            order.isDelete = false;
            order.CreatorId = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
            order.CreatorName = JwtHelper.GetName(_httpContextAccessor.HttpContext);
            
            await _repository.AddAsync(order);
          return new ApiResult<RepairOrderAddDto> { Data = dto, Message = "创建成功" };
    }

    /// <summary>
    /// 删除维修单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> DeleteAsync(long id)
    {
        bool deleted = await _repository.DeleteAsync(id);
        if (deleted)
          return new ApiResult<bool> { Data = true, Message = "删除成功" };
        else
          return new ApiResult<bool> { Data = false, Message = "删除失败" };
    }

    #region 修改
    /// <summary>
    /// 修改维修单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<RepairOrderUpdateDto>> UpdateAsync(RepairOrderUpdateDto dto)
    {
        var existingOrder= await _repository.FirstOrDefaultAsync(c => c.Id.Equals(dto.Id));
        _mapper.Map(dto, existingOrder);
        existingOrder.Status = Status.待审核;
        existingOrder.UpdateTime = DateTime.Now;
        await _repository.UpdateAsync(existingOrder);
        return new ApiResult<RepairOrderUpdateDto> { Data = dto, Message = "更新成功" };
    }

    /// <summary>
    /// 管理员审核通过维修单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> ApproveOrderAsync(long id)
    {       
        var order = await _repository.FindAsync(id);
        order.Status = Status.待派单;
        await _repository.UpdateAsync(order);
        //删除所有维修的审核日志
        var logs = await _context.AuditLog.Where(log => log.RepairOrderId.Equals(id)).ToListAsync();
        _context.AuditLog.RemoveRange(logs);
        await _context.SaveChangesAsync();

        return new ApiResult<bool> { Data = true, Message = "更新成功"};
    }

    /// <summary>
    /// 管理员派单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> DispatchOrderAsync(DispatchOrderDTO dto)
    {
         var order = await _repository.FindAsync(dto.OrderId);
         order.RepairWorkerId = dto.RepairWorkerId;
         order.Status = Status.待处理;
         order.OrderType = dto.OrderType;
         await _repository.UpdateAsync(order);
        return new ApiResult<bool> { Data = true, Message = "更新成功" };
    }

    /// <summary>
    /// 维修工拒接
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> RefuseOrderAsync(long orderId)
    {
        var order = await _repository.FindAsync(orderId);
        order.Status = Status.被拒绝;
        await _repository.UpdateAsync(order);
        return new ApiResult<bool> { Data = true, Message = "更新成功" };
    }

    /// <summary>
    /// 维修工接单
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> TakeOrderAsync(long orderId)
    {
        var order = await _repository.FindAsync(orderId);
        order.Status = Status.处理中;
        await _repository.UpdateAsync(order);      
        return new ApiResult<bool> { Data = true, Message = "更新成功" };
    }

    /// <summary>
    /// 维修工结单
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> FinishOrderAsync(long orderId)    
    {
        var order = await _repository.FindAsync(orderId);
        order.Status = Status.已处理;
        await _repository.UpdateAsync(order);
        return new ApiResult<bool> { Data = true, Message = "更新成功" };
    }

    #endregion
    
    #region 查询
    /// <summary>
    /// 编辑维修单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<RepairOrderUpdateDto>> EditAsync(long id)
    {
        var order = await _repository.FindAsync(id);
        var dtos = _mapper.Map<RepairOrderUpdateDto>(order);
        return new ApiResult<RepairOrderUpdateDto> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取用户待审核的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetTobeAuditOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var query = from order in _context.RepairOrder
                    join log in _context.AuditLog on order.Id equals log.RepairOrderId into logs
                    from log in logs.DefaultIfEmpty()
                    join area in _context.Area on order.AreaId equals area.Id
                    where ((log == null || log.CreateTime == _context.AuditLog
                        .Where(al => al.RepairOrderId.Equals(order.Id))
                        .Max(al => al.CreateTime))
                        && order.CreatorId.Equals(id) && order.Status.Equals(Status.待审核) || order.Status.Equals(Status.审核失败))
                    select new RepairOrderUserDisplayDto
                    {
                        Id = order.Id,
                        SpecificNumber = order.SpecificNumber,
                        Description = order.Description,
                        RepairTime = order.RepairTime,
                        ImageUrls = order.ImageUrls,
                        AreaName = area.AreaName,
                        OrderTypeDescription = EnumException.GetDescription(order.OrderType),
                        StatusDescription = EnumException.GetDescription(order.Status),
                        LatestSuggestion = log != null ? log.Suggestion : null
                    };

//            var result = await PageHelper<RepairOrderUserDisplayDto>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
//
//            PageData<RepairOrderUserDisplayDto> pageData = new()
//            {
//                PageIndex = queryParameter.PageIndex,
//                PageSize = queryParameter.PageSize,
//                List = result,
//                Total = query.Count()
//            };

        return new ApiResult<List<RepairOrderUserDisplayDto>> { Data = query.ToList(), Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取用户的待处理的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetTobeFinishedOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var query = _repository.Query(c => c.CreatorId.Equals(id)
              && c.IsRated.Equals(false)
              && c.Status.Equals(Status.处理中) || c.Status.Equals(Status.待派单) || c.Status.Equals(Status.被拒绝))
            .Include(c => c.Area);
       // var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
        var dtos = _mapper.Map<List<RepairOrderUserDisplayDto>>(query);
//        PageData<RepairOrderUserDisplayDto> pageData = new()
//        {
//            PageIndex = queryParameter.PageIndex,
//            PageSize = queryParameter.PageSize,
//            List = dtos,
//            Total = query.Count()
//        };

        return new ApiResult<List<RepairOrderUserDisplayDto>> { Data = dtos, Message = "查询成功" };
    }


    /// <summary>
    /// 分页获取用户的未评价的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetNotRatingOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var query =  _repository.Query(c => c.CreatorId.Equals(id)
              && c.IsRated.Equals(false)
              && c.Status.Equals(Status.已处理))
            .Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
        var dtos = _mapper.Map<List<RepairOrderUserDisplayDto>>(query);
//        PageData<RepairOrderUserDisplayDto> pageData = new()
//        {
//            PageIndex = queryParameter.PageIndex,
//            PageSize = queryParameter.PageSize,
//            List = dtos,
//            Total = query.Count()
//        };

        return new ApiResult<List<RepairOrderUserDisplayDto>> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取用户的完成并已评价的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderUserDisplayDto>>> GetRatedOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var query = _repository.Query(c => c.CreatorId.Equals(id)
              && c.IsRated.Equals(true))
            .Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
        var dtos = _mapper.Map<List<RepairOrderUserDisplayDto>>(query);
//        PageData<RepairOrderUserDisplayDto> pageData = new()
//        {
//            PageIndex = queryParameter.PageIndex,
//            PageSize = queryParameter.PageSize,
//            List = dtos,
//            Total = query.Count()
//        };

        return new ApiResult<List<RepairOrderUserDisplayDto>> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取维修人员的待处理的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderWorkerDisplayDto>>> GetUnTakeOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var query = _repository.Query(c => c.RepairWorkerId.Equals(id) && c.Status.Equals(Status.待处理)).Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);

        var dtos = _mapper.Map<List<RepairOrderWorkerDisplayDto>>(query);
        /*PageData<RepairOrderWorkerDisplayDto> pageData = new()
        {
            PageIndex = queryParameter.PageIndex,
            PageSize = queryParameter.PageSize,
            List = dtos,
            Total = query.Count()
        };*/

        return new ApiResult<List<RepairOrderWorkerDisplayDto>> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取维修人员的处理中的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderWorkerDisplayDto>>> GetPendingOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var query =  _repository.Query(c => c.RepairWorkerId.Equals(id) && c.Status.Equals(Status.处理中)).Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);

        var dtos = _mapper.Map<List<RepairOrderWorkerDisplayDto>>(query);
/*        PageData<RepairOrderWorkerDisplayDto> pageData = new()
        {
            PageIndex = queryParameter.PageIndex,
            PageSize = queryParameter.PageSize,
            List = dtos,
            Total = query.Count()
        };*/

        return new ApiResult<List<RepairOrderWorkerDisplayDto>> { Data = dtos , Message = "查询成功"};
    }

    /// <summary>
    /// 分页获取维修人员的所有完成的订单
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderWorkerDisplayDto>>> GetRatedByUserOrderPagedListAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();

        //var unratedQuery = _repository.Query(c => c.RepairWorkerId.Equals(id) && c.IsRated.Equals(false)).Include(c => c.Area);
        //var ratedQuery = _repository.Query(c => c.RepairWorkerId.Equals(id) && c.IsRated.Equals(true)).Include(c => c.Area);

        //var combinedQuery = unratedQuery.Concat(ratedQuery).AsQueryable();
        var query = _repository.Query(c => c.RepairWorkerId.Equals(id) && (c.Status.Equals(Status.已处理) || c.Status.Equals(Status.已完成))).OrderByDescending(c => c.IsRated).Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
        var dtos = _mapper.Map<List<RepairOrderWorkerDisplayDto>>(query);
        /*PageData<RepairOrderWorkerDisplayDto> pageData = new()
        {
            PageIndex = queryParameter.PageIndex,
            PageSize = queryParameter.PageSize,
            List = dtos,
            Total = *//*unratedQuery.Count() + ratedQuery.Count()*//* query.Count(),
        };*/

        return new ApiResult<List<RepairOrderWorkerDisplayDto>> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取待审核的订单
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderAssignmentDto>>> GetTobeReviewedOrderPagedListAsync()
    {
        var query = _repository.Query(c => c.Status.Equals(Status.待审核) || c.Status.Equals(Status.审核失败)).Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);

        var dtos = _mapper.Map<List<RepairOrderAssignmentDto>>(query);
//        PageData<RepairOrderAssignmentDto> pageData = new()
//        {
//            PageIndex = queryParameter.PageIndex,
//            PageSize = queryParameter.PageSize,
//            List = dtos,
//            Total = query.Count()
//        };

        return new ApiResult<List<RepairOrderAssignmentDto>> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取待派单的订单
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderAssignmentDto>>> GetUnTakeByAdminOrderPagedListAsync()
    {
        var query = _repository.Query(c => c.Status.Equals(Status.待派单) || c.Status.Equals(Status.被拒绝)).Include(c => c.Area);
        //var result = await PageHelper<RepairOrder>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);

        var dtos = _mapper.Map<List<RepairOrderAssignmentDto>>(query);
//        PageData<RepairOrderAssignmentDto> pageData = new()
//        {
//            PageIndex = queryParameter.PageIndex,
//            PageSize = queryParameter.PageSize,
//            List = dtos,
//            Total = query.Count()
//        };

        return new ApiResult<List<RepairOrderAssignmentDto>> { Data = dtos, Message = "查询成功" };

    }

    /// <summary>
    /// 分页获取所有未完成订单信息
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderAdminDisplayDto>>> GetAllUnFinishedOrderPagedListAsync()
    {
        var query = from order in _context.RepairOrder
                    join area in _context.Area on order.AreaId equals area.Id
                    where !order.Status.Equals(Status.已完成)
                    join user in _context.AppUser on order.RepairWorkerId equals user.Id into users
                    from repairWorker in users.DefaultIfEmpty()
                    join comment in _context.Comment on order.Id equals comment.RepairOrderId into comments
                    from repairComment in comments.DefaultIfEmpty()
                    select new RepairOrderAdminDisplayDto
                    {
                        Id = order.Id,
                        StudentNumber = order.StudentNumber,
                        SpecificNumber = order.SpecificNumber,
                        Description = order.Description,
                        RepairTime = order.RepairTime,
                        PhoneNumber = order.PhoneNumber,
                        ImageUrls = order.ImageUrls,
                        AreaName = area.AreaName,
                        OrderTypeDescription = EnumException.GetDescription(order.OrderType),
                        StatusDescription = EnumException.GetDescription(order.Status),
                        RepairWorkerName = repairWorker != null ? repairWorker.Name : null,
                        Rating = repairComment != null ? repairComment.Rating : 0,
                        CommentText = repairComment != null ? repairComment.CommentText : null
                    };

           /*var result = await PageHelper<RepairOrderAdminDisplayDto>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
            PageData<RepairOrderAdminDisplayDto> pageData = new()
            {
                PageIndex = queryParameter.PageIndex,
                PageSize = queryParameter.PageSize,
                List = result,
                Total = query.Count()
            };*/
            return new ApiResult<List<RepairOrderAdminDisplayDto>>{Data = query.ToList(),Message = "查询成功"};
    }

    /// <summary>
    /// 分页获取所有完成订单信息
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairOrderAdminDisplayDto>>> GetAllFinishedOrderPagedListAsync()
    {
        var query = from order in _context.RepairOrder
                    join area in _context.Area on order.AreaId equals area.Id
                    where order.Status.Equals(Status.已完成)
                    join user in _context.AppUser on order.RepairWorkerId equals user.Id into users
                    from repairWorker in users.DefaultIfEmpty()
                    join comment in _context.Comment on order.Id equals comment.RepairOrderId into comments
                    from repairComment in comments.DefaultIfEmpty()
                    select new RepairOrderAdminDisplayDto
                    {
                        Id = order.Id,
                        StudentNumber = order.StudentNumber,
                        SpecificNumber = order.SpecificNumber,
                        Description = order.Description,
                        RepairTime = order.RepairTime,
                        PhoneNumber = order.PhoneNumber,
                        ImageUrls = order.ImageUrls,
                        AreaName = area.AreaName,
                        OrderTypeDescription = EnumException.GetDescription(order.OrderType),
                        StatusDescription = EnumException.GetDescription(order.Status),
                        RepairWorkerName = repairWorker != null ? repairWorker.Name : null,
                        Rating = repairComment != null ? repairComment.Rating : 0,
                        CommentText = repairComment != null ? repairComment.CommentText : null
                    };

//        var result = await PageHelper<RepairOrderAdminDisplayDto>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
//        PageData<RepairOrderAdminDisplayDto> pageData = new()
//        {
//            PageIndex = queryParameter.PageIndex,
//            PageSize = queryParameter.PageSize,
//            List = result,
//            Total = query.Count()
//        };
        return new ApiResult<List<RepairOrderAdminDisplayDto>> { Data = query.ToList(), Message = "查询成功" };
    }
    #endregion
}