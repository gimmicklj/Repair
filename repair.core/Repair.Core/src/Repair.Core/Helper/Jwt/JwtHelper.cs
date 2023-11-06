using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Repair.Core.Extensions.String;
using Repair.Core.Helper.Time;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repair.Core.Helper.Jwt;

public static class JwtHelper
{
    /// <summary>
    /// 生成token令牌
    /// </summary>
    /// <param name="claimInfo"></param>
    /// <param name="tokenManagement"></param>
    /// <returns></returns>
    public static string CreateToken(Claim[] claimInfo, TokenManagement tokenManagement)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var notTime = DateTimeHelper.GetThisDateTime(); //开始时间

        var expiresTime = notTime.AddMinutes(tokenManagement.AccessExpiration);    //到期时间

        var jwtToken = new JwtSecurityToken(tokenManagement.Issuer
            , tokenManagement.Audience
            , claimInfo
            , notTime
            , expiresTime
            , credentials);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return token;
    }
    /// <summary>
    /// 获取请求上下文Claims中的UserId
    /// </summary>
    /// <param name="httpContext">请求上下文</param>
    /// <returns></returns>
    public static string GetUserId(HttpContext httpContext)
    {
        string userId = "";
        if (httpContext.User is null)
            return userId;
        if (!httpContext.User.Claims.Any())
            return userId;

        var claim = httpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Sid);
        if (claim is null)
            return userId;

        userId = claim.Value;
        return userId;
    }
    /// <summary>
    /// 获取请求上下文Claims中的Name名称
    /// </summary>
    /// <param name="httpContext">请求上下文</param>
    /// <returns></returns>
    public static string GetName(HttpContext httpContext)
    {
        string name = "";
        if (httpContext.User is null)
            return name;
        
        if (!httpContext.User.Claims.Any())
            return name;

        var claim = httpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Name);
        if (claim is null)
            return name;
        name = claim.Value;

        return name;
    }
    /// <summary>
    /// 解析token获取JwtSecurityToken对象
    /// </summary>
    /// <param name="token">token</param>
    /// <returns></returns>
    public static JwtSecurityToken GetSecurityToken(string token)
    {
        token = GetRemoveBearerToken(token);
        //解析token
        JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return jwtToken;
    }
    
    /// <summary>
    /// 获取token中存储的信息
    /// </summary>
    /// <param name="httpContext">http数据上下文</param>
    /// <returns></returns>
    public static TokenPayload GetTokenPayload(HttpContext httpContext)
    {
        var token = GetRemoveBearerToken(httpContext.Request.Headers["Authorization"].ToString());

        //解析token
        JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        //获取token中的payload信息
        TokenPayload tokenPayload = new()
        {
            Id = Convert.ToInt32(jwtToken.Payload[ClaimTypes.Sid]),
            Email = jwtToken.Payload[ClaimTypes.Email].ToString(),
            UserName = jwtToken.Payload[ClaimTypes.Surname].ToString(),
            Name = jwtToken.Payload[ClaimTypes.Name].ToString(),
            Role = jwtToken.Payload[ClaimTypes.Role].ToString(),
        };
        return tokenPayload;
    }
    /// <summary>
    /// 获取token中存储的信息
    /// </summary>
    /// <param name="token">token令牌</param>
    /// <returns></returns>
    public static TokenPayload GetTokenPayload(string token)
    {
        token = GetRemoveBearerToken(token);

        //解析token
        JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        //获取token中的payload信息
        TokenPayload tokenPayload = new()
        {
            Id = Convert.ToInt32(jwtToken.Payload[ClaimTypes.Sid]),
            Email = jwtToken.Payload[ClaimTypes.Email].ToString(),
            UserName = jwtToken.Payload[ClaimTypes.Surname].ToString(),
            Name = jwtToken.Payload[ClaimTypes.Name].ToString(),
        };
        if (jwtToken.Payload[ClaimTypes.Role] != null)
            tokenPayload.Role = jwtToken.Payload[ClaimTypes.Role].ToString();

        return tokenPayload;
    }
    /// <summary>
    /// 获取去除字符串开头Bearer的token令牌
    /// </summary>
    /// <param name="token">token令牌</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetRemoveBearerToken(string token)
    {
        if (token.IsNullOrEmpty())
            throw new ArgumentNullException("传入的token为空!");

        if (token.StartsWith("Bearer "))
            return token.Replace("Bearer ", "");

        return token;
    }
}