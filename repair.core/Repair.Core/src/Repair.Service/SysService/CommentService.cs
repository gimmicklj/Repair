using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repair.Core.Helper.Jwt;
using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Repository.BusRepository;
using Repair.Core.Extensions.String;
using Repair.Entity.Dto.SysDto.Comment;
using Repair.Entity.SysEntity.Enum;
using Repair.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repair.Service.SysService;

public class CommentService : BaseService<Comment>, ICommentService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public CommentService(ICommentRepository commentRepository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(commentRepository)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _context = _repository.GetDbContext();
    }

    /// <summary>
    /// 增加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<CommentAddDto>> AddAsync(CommentAddDto dto)
    {
        var query = await _context.RepairOrder.FirstOrDefaultAsync(c => c.Id == dto.RepairOrderId && c.Status == Status.已处理);
        if (query != null)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.CreateTime = DateTime.Now;
            comment.UpdateTime = DateTime.Now;
            comment.isDelete = false;
            comment.CreatorId = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
            comment.CreatorName = JwtHelper.GetName(_httpContextAccessor.HttpContext);
            await _repository.AddAsync(comment);
            var order = await _context.RepairOrder.FirstOrDefaultAsync(c => c.Id.Equals(dto.RepairOrderId));
            _context.Attach(order);
            order.IsRated = true;
            order.Status = Status.已完成;
            await _context.SaveChangesAsync();
            return new ApiResult<CommentAddDto> { Data = dto, Message = "评分完成" };
        }

        return new ApiResult<CommentAddDto> { Code = -1, Message = "订单未完成" };
    }

    /// <summary>
    /// 查看评价
    /// </summary>
    /// <param name="repairOrderId"></param>
    /// <returns></returns>
    public async Task<ApiResult<CommentDto>> GetAsync(long repairOrderId)
    {
        var appUser = await _repository.FirstOrDefaultAsync(c => c.RepairOrderId.Equals(repairOrderId));
        var dtos = _mapper.Map<CommentDto>(appUser);
        return new ApiResult<CommentDto> { Data = dtos, Message = "查询成功" };
    }
}
