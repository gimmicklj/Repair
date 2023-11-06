using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repair.Entity;

namespace Repair.Api.Filters;

/// <summary>
/// 全局异常拦截器
/// </summary>
public class GlobalExceptionFilter : ExceptionFilterAttribute
{

    private readonly ILogger<GlobalExceptionFilter> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        // 如果异常没有被处理则进行处理
        if (context.ExceptionHandled == false)
        {

            ApiResult<string> res = new ();
            res.Code = 500;
            res.Message = "发生错误,请联系管理员";
            //写入日志
            _logger.LogError(context.HttpContext.Request.Path, context.Exception);

            context.Result = new ContentResult
            {
                // 返回状态码设置为200，表示成功
                StatusCode = StatusCodes.Status200OK,
                // 设置返回格式
                ContentType = "application/json;charset=utf-8",
                Content = JsonConvert.SerializeObject(res)
            };
        }
        // 设置为true，表示异常已经被处理了
        context.ExceptionHandled = true;
    }
}

