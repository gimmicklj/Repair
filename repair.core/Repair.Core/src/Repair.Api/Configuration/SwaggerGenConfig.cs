using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Repair.Api.Filters;
namespace Repair.Api.Configuration;

/// <summary>
/// Swagger配置类
/// </summary>
public static class SwaggerGenConfig
{
    /// <summary>
    /// 静态方法
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigSwaggerGen(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        var config = serviceProvider.GetRequiredService<IConfiguration>();

        // 文档
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = config["ApiTitle"] ?? " Repair.Api", Version = "v1" });

            // 添加注释说明
            var xmlFolderPath = Path.Combine(env.ContentRootPath, "bin", "Debug", "net6.0");
            if (Directory.Exists(xmlFolderPath))
            {
                var files = Directory.GetFiles(xmlFolderPath, "*.xml");

                foreach (var file in files)
                {
                    c.IncludeXmlComments(file);
                }
            }


            // 添加控制器注释
            c.TagActionsBy(api =>
            {
                var controllerActionDescriptor = (ControllerActionDescriptor)api.ActionDescriptor;
                var tag = controllerActionDescriptor.ControllerName + "-" + controllerActionDescriptor.ControllerTypeInfo.GetDisplayName();
                return new List<string>()
                {
                    tag
                };
            });
            //添加自定义操作筛选器
            c.OperationFilter<FileUploadOperationFilter>();

            var bearerScheme = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            };

            c.AddSecurityDefinition("Bearer", bearerScheme);

            // 这个要加上，否则请求的时候头部不会带Authorization
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme{ Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "Bearer"}},
                    new List<string>()
                }
            });
        });

        //启用对 Newtonsoft.Json 的支持
        services.AddSwaggerGenNewtonsoftSupport();
    }
}
