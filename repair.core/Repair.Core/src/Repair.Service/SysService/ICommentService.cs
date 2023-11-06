using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Entity.Dto.SysDto.Comment;

namespace Repair.Service.SysService;

public interface ICommentService: IBaseService<Comment>
{
    Task<ApiResult<CommentAddDto>> AddAsync(CommentAddDto dto);

    Task<ApiResult<CommentDto>> GetAsync(long repairOrderId);
}
