using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repair.Entity.Dto.SysDto.Comment;
using Repair.Service.SysService;
using System.ComponentModel;

namespace Repair.Api.Controllers;

/// <summary>
/// 评论控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("评论接口")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="commentService"></param>
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// 添加评分
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] CommentAddDto dto)
    {
        return Ok(await _commentService.AddAsync(dto));
    }

    /// <summary>
    /// 查看评价
    /// </summary>
    /// <param name="repairOrderId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> GetAsync(long repairOrderId)
    {
        return Ok(await _commentService.GetAsync(repairOrderId));
    }
}
