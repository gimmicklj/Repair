using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Repository.BusRepository;
using AutoMapper;
using Repair.Core.Helper.Jwt;
using Microsoft.AspNetCore.Http;
using Repair.Core.Extensions.String;
using Microsoft.EntityFrameworkCore;
using Repair.Entity.Dto.SysDto.AppUser;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.EntityFrameworkCore.Collections;
using Repair.Entity.Dto.BusDto.Echart;
using Repair.Entity.SysEntity.Enum;
using Repair.EntityFrameworkCore;

namespace Repair.Service.SysService;

public class AppUserService : BaseService<AppUser>, IAppUserService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public AppUserService(IAppUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(userRepository)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _context = _repository.GetDbContext();
    }

    /// <summary>
    /// 验证用户信息
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<AppUser> VerifyUserAsync(string userName, string passWord)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
        {
            throw new ArgumentNullException("请输入用户名或密码");
        }
        var user = await _repository.FirstOrDefaultAsync(e => e.UserName.Equals(userName) &&
           e.Password.Equals(passWord) && e.isDelete == false);
        return user;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> DeleteAsync(long id)
    {
        var query = await _repository.FindAsync(id);
        query.isDelete = true;
        await _repository.UpdateAsync(query);
        return new ApiResult<bool> { Data = true, Message = "删除成功" };

    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<AppUserUpdateDto>> EditAppUserInfoAsync(long id)
    {
        var appUser = await _repository.FirstOrDefaultAsync(c => c.Id.Equals(id));
        var dtos = _mapper.Map<AppUserUpdateDto>(appUser);
        return new ApiResult<AppUserUpdateDto> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<AppUserUpdateDto>> UpdateAsync(AppUserUpdateDto dto)
    {
        var existingAppUser = await _repository.FindAsync(dto.Id);
        _mapper.Map(dto, existingAppUser);
        existingAppUser.UpdateTime = DateTime.Now;
        await _repository.UpdateAsync(existingAppUser);
        return new ApiResult<AppUserUpdateDto> { Data = dto, Message = "更新成功" };
    }

    #region 查询
    /// <summary>
    /// 获取个人信息
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<AppUserDto>> GetAppUserInfoAsync()
    {
        var id = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
        var appUser = await _repository.FirstOrDefaultAsync(c => c.Id.Equals(id) && c.isDelete.Equals(false));
        var dtos = _mapper.Map<AppUserDto>(appUser);
        return new ApiResult<AppUserDto> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 获取维修工下拉框
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<RepairWorkerSelectDto>>> GetRepairWorkerSelectAsync()
    {
        var workers = await _repository.Query(c => c.RoleName.Equals("维修人员") && c.isDelete.Equals(false)).ToListAsync();
        var workerDtos = _mapper.Map<List<RepairWorkerSelectDto>>(workers);
        return new ApiResult<List<RepairWorkerSelectDto>> { Data = workerDtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页获取用户信息
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<AppUserDto>>> GetPagedListAppUser()
    {
        var query = _repository.Query(c => c.isDelete.Equals(false) && c.isDelete.Equals(false) && !c.RoleId.Equals(RoleType.管理员));
        var dtos = _mapper.Map<List<AppUserDto>>(query);
        
        return new ApiResult<List<AppUserDto>> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 获取维修工的订单情况
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<EmployeeEvaluationDto>>> GetEmployeeEvaluation()
    {
        var employeeEvaluationCounts = await _context.AppUser
        .Where(u => u.RoleId.Equals(RoleType.维修人员) && u.isDelete.Equals(false))
        .Select(u => new EmployeeEvaluationDto
        {
            EmployeeId = u.Id,
            Name = u.Name,
            GoodCount = u.RepairOrders.Count(ro => ro.RepairWorkerId == u.Id && ro.Comment != null && ro.Comment.Rating >= 4),
            AverageCount = u.RepairOrders.Count(ro => ro.RepairWorkerId == u.Id && ro.Comment != null && ro.Comment.Rating >= 2 && ro.Comment.Rating <= 3),
            PoorCount = u.RepairOrders.Count(ro => ro.RepairWorkerId == u.Id && ro.Comment != null && ro.Comment.Rating < 2)
        })
        .ToListAsync();
        return new ApiResult<List<EmployeeEvaluationDto>> { Data = employeeEvaluationCounts, Message = "查询成功" };
    }
    #endregion
}
