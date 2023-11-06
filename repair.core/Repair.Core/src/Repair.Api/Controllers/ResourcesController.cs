using Microsoft.AspNetCore.Mvc;
using Repair.Core.Helper.IO;
using Repair.Entity;
using Repair.Entity.BusEntity;
using System.ComponentModel;

namespace Repair.Api.Controllers;

/// <summary>
/// 资源上传控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[DisplayName("资源上传接口")]
public class ResourcesController : ControllerBase
{
    private readonly ILogger<ResourcesController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    /// <summary>
    /// 依赖注入
    /// </summary>
    /// <param name="webHostEnvironment"></param>
    /// <param name="logger"></param>
    public ResourcesController(IWebHostEnvironment webHostEnvironment, ILogger<ResourcesController> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    /// <summary>
    /// 图片批量上传
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<ApiResult<List<ResoureceFileResult>>> BatchUploadAsync()
    {
        List<ResoureceFileResult> result = new();
        var files = Request.Form.Files;
        const string fileFilt = ".gif|.jpg|.jpeg|.png";
        foreach (var itemFile in files)
        {
            if (!fileFilt.Contains(itemFile.GetFileType()))
            {
                return new ApiResult<List<ResoureceFileResult>>
                {
                    Code = -1,
                    Message = "文件不支持"
                };
            }
        }

        #region 创建文件夹
        var directory = $"Images/{DateTime.Now.ToString("yyyy_MM_dd")}";
        var uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, directory);

        _logger.LogInformation("执行创建文件夹");
        DirectoryHelper.CreateIfNotExists(uploadDirectory);
        _logger.LogInformation("执行创建文件夹成功");
        #endregion

        #region 批量上传
        foreach (var itemFile in Request.Form.Files)
        {
            var fType = itemFile.GetFileType();
            var fName = itemFile.GetFileName();
            var fullFName = $"{fName}.{fType}";
            var thumbFName = $"{fName}_thumb";
            var fullthumbFName = $"{thumbFName}.{fType}";

            var uploadUrl = $"{uploadDirectory}/{fullFName}";
            _logger.LogInformation("执行创建文件");
            using (var stream = new FileStream(uploadUrl, FileMode.Create))
            {
                await itemFile.CopyToAsync(stream);
            }
            _logger.LogInformation("执行创建成功");

            //生成缩略图
            // var thumbUploadUrl = $"{uploadDirectory}/{fullthumbFName}";
            // ImagesCondenseExtension.CompressImage(uploadUrl, thumbUploadUrl);

            result.Add(new ResoureceFileResult()
            {
                OldFileName = itemFile.FileName,
                Type = fType,
                FileName = fName,
                Url = $"https://{Request.Host}/{directory}/{itemFile.FileName}",
                ThumbUrl = $"http://{Request.Host}/{directory}/{fullthumbFName}"
            });
        }
        #endregion

        return new ApiResult<List<ResoureceFileResult>> { Message = "上传成功", Data = result };
    }

}
