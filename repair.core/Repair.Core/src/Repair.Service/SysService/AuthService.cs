using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repair.Core.Extensions.Enum;
using Repair.Core.Helper.Encrypts;
using Repair.Core.Helper.Jwt;
using Repair.Entity;
using Repair.Entity.Dto.BusDto.Auth;
using Repair.Entity.Dto.SysDto.AppUser;
using Repair.Entity.SysEntity;
using System.Security.Claims;

namespace Repair.Service.SysService;

public class AuthService : IAuthService
{
    private readonly IAppUserService _appUserService;
    private readonly IOptions<TokenManagement> _tokenManagement;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public AuthService(IAppUserService appUserService,
        IOptions<TokenManagement> tokenManagement, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _appUserService = appUserService;
        _tokenManagement = tokenManagement;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    /// <returns></returns>
    public async Task<ApiResult<AuthDto>> LoginAsync(string userName, string passWord)
    {
        var user = await _appUserService.VerifyUserAsync(userName, EncryptHelper.GetMD5(passWord));
        if (user == null)       
            return new ApiResult<AuthDto>{Code = -1,Message = "用户不存在"};
        
        List<Claim> claimList = new()
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Surname, user.UserName.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString()),
            new Claim(ClaimTypes.Name, user.Name.ToString()),
            new Claim(ClaimTypes.Role, user.RoleName)
        };

        string token = JwtHelper.CreateToken(claimList.ToArray(), _tokenManagement.Value);
        if (string.IsNullOrEmpty(token))       
            return new ApiResult<AuthDto>{Code = -1,Message = "用户信息验证异常"};
        
        return new ApiResult<AuthDto>
        {
            Message = "系统用户登录成功",
            Data = new AuthDto
            {
                VerifyResult = true,
                Token = token,
                Data = new
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    Email = user.Email,
                    RoleName = user.RoleName,
                }
            }
        };
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<AppUserAddDto>> RegisterAsync(AppUserAddDto dto)
    {
        var existingUser = await _appUserService.FirstOrDefaultAsync(c => c.UserName.Equals(dto.UserName));
        dto.Password = EncryptHelper.GetMD5(dto.Password);
        if (existingUser != null)
        {
            return new ApiResult<AppUserAddDto> { Code = -1, Message = "用户名已存在" };
        }

        var newUser = _mapper.Map<AppUser>(dto);
        newUser.CreateTime = DateTime.Now;
        newUser.UpdateTime = DateTime.Now;
        newUser.RoleName = EnumException.GetDescription(dto.RoleId);
        newUser.RoleId = dto.RoleId;
        newUser.CreatorName = dto.Name;
        newUser.isDelete = false;   

        var result = await _appUserService.AddAsync(newUser);
        if (result)
        {
            newUser.CreatorId = newUser.Id;
            await _appUserService.UpdateAsync(newUser);
        }
        return new ApiResult<AppUserAddDto> { Message = "注册成功", Data = dto };
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <returns></returns>
    public ApiResult<bool> LoginOut()
    {
        string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        JwtHelper.GetRemoveBearerToken(authorizationHeader);
        return new ApiResult<bool> { Data = true, Message = "退出登录成功" };
    }
}