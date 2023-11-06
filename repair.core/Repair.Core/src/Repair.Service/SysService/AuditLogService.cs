using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repair.Core.Helper.Jwt;
using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Repository.BusRepository;
using Repair.Core.Extensions.String;
using Repair.EntityFrameworkCore;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.AuditLog;
using Repair.EntityFrameworkCore.Collections;
using Microsoft.EntityFrameworkCore;
using Repair.Entity.SysEntity.Enum;

namespace Repair.Service.SysService;

public class AuditLogService : BaseService<AuditLog>, IAuditLogService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditLogService(IAuditLogRepository auditLogRepository, IMapper mapper,
       IHttpContextAccessor httpContextAccessor) : base(auditLogRepository)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _context = auditLogRepository.GetDbContext();
    }

    /// <summary>
    /// 添加审核日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<AuditLogAddDto>> AddAsync(AuditLogAddDto dto)
    {
            var log= _mapper.Map<AuditLog>(dto);
            log.CreateTime = DateTime.Now;
            log.UpdateTime = DateTime.Now;
            log.isDelete = false;
            log.CreatorId = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
            log.CreatorName = JwtHelper.GetName(_httpContextAccessor.HttpContext);
            await _repository.AddAsync(log);
            var order = await _context.RepairOrder.FirstOrDefaultAsync(c => c.Id.Equals(dto.RepairOrderId));
            _context.Attach(order);
            order.Status = Status.审核失败;
            await _context.SaveChangesAsync();
        return new ApiResult<AuditLogAddDto> { Message = "创建成功", Data = dto };
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> DeleteAsync(long id)
    {
        bool deleted = await _repository.DeleteAsync(id);
        if (deleted)
            return new ApiResult<bool> { Data = true, Message = "删除成功" };
        else
            return new ApiResult<bool> { Code = -1, Data = false, Message = "删除失败" };
    }

    /// <summary>
    /// 删除指定订单的所有日志
    /// </summary>
    /// <param name="orderId">订单ID</param>
    /// <returns></returns>
    public bool DeleteAllLogsByOrderId(long orderId)
    {
        var logs =  _repository.Query(log => log.RepairOrderId.Equals(orderId));
       if(_repository.DeleteRange(logs))
        return true;
       else return false;
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<AuditLogUpdateDto>> EditAsync(long id)
    {
        var log = await _repository.FirstOrDefaultAsync(c => c.Id.Equals(id));
        var dtos = _mapper.Map<AuditLogUpdateDto>(log);
        return new ApiResult<AuditLogUpdateDto> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<AuditLogUpdateDto>> UpdateAsync(AuditLogUpdateDto dto)
    {
        var log = await _repository.FirstOrDefaultAsync(c => c.Id.Equals(dto.Id));     
        _mapper.Map(dto, log);
        log.UpdateTime = DateTime.Now;
        await _repository.UpdateAsync(log);
        return new ApiResult<AuditLogUpdateDto> { Data = dto, Message = "更新成功" };
    }

    /// <summary>
    /// 分页展示审核日志
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns></returns>
    public async Task<ApiResult<List<AuditLogDto>>> GetAuditLogPagedListAsync(long OrderId)
    {
        var query = from log in _context.AuditLog
                    join order in _context.RepairOrder on log.RepairOrderId equals order.Id
                    where log.RepairOrderId.Equals(OrderId)
                    select new AuditLogDto
                    {
                        Id = log.Id,
                        Suggestion = log.Suggestion,
                        SpecificNumber = order.SpecificNumber,
                        CreateTime = log.CreateTime,
                        CreatorName = log.CreatorName
                    };
//        var result = await PageHelper<AuditLogDto>.ToPageListAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
//            PageData<AuditLogDto> pageData = new ()
//            {
//                PageIndex = queryParameter.PageIndex,
//                PageSize = queryParameter.PageSize,
//                List = result,
//                Total = query.Count()
//            };
            return new ApiResult<List<AuditLogDto>> { Data = query.ToList(), Message = "查询成功" };
    }   
}
