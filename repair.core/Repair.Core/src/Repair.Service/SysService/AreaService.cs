using Repair.Entity;
using Repair.Entity.SysEntity;
using Repair.Repository.BusRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repair.Core.Helper.Jwt;
using Microsoft.EntityFrameworkCore;
using Repair.Core.Extensions.String;
using Repair.Entity.Dto.BusDto.Parameters;
using Repair.Entity.Dto.SysDto.Area;

namespace Repair.Service.SysService;

public class AreaService: BaseService<Area>, IAreaService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AreaService(IAreaRepository areaRepository,IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(areaRepository)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<AreaAddDto>> AddAsync(AreaAddDto dto)
    {
            var area = _mapper.Map<Area>(dto);
            area.CreateTime = DateTime.Now;
            area.UpdateTime = DateTime.Now;
            area.isDelete = false;
            area.CreatorId = JwtHelper.GetUserId(_httpContextAccessor.HttpContext).ToLongOrDefault();
            area.CreatorName = JwtHelper.GetName(_httpContextAccessor.HttpContext);
            await _repository.AddAsync(area);
            return new ApiResult<AreaAddDto>{Data = dto,Message = "添加成功"};       
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<bool>> DeleteAsync(long id)
    {
        var query = await _repository.FindAsync(id);
        if (query == null)
        {
            return new ApiResult<bool> { Data = false, Message = "区域不存在" };
        }
        bool deleted = await _repository.DeleteAsync(id);
            if (deleted)          
               return new ApiResult<bool> {Data = true, Message = "删除成功" };           
            else           
               return new ApiResult<bool> { Code = -1, Data = false, Message = "删除失败" };                  
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<AreaUpdateDto>> EditAsync(long id)
    {
        var area = await _repository.FirstOrDefaultAsync(c => c.Id.Equals(id));
        var dtos = _mapper.Map<AreaUpdateDto>(area);
        return new ApiResult<AreaUpdateDto> { Data = dtos, Message = "查询成功" };
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ApiResult<AreaUpdateDto>> UpdateAsync(AreaUpdateDto dto)
    {
            var existingArea = await _repository.FirstOrDefaultAsync( c=>c.Id.Equals(dto.Id));
                _mapper.Map(dto, existingArea);
                existingArea.UpdateTime = DateTime.Now;
                await _repository.UpdateAsync(existingArea);                 
              return new ApiResult<AreaUpdateDto> {Data = dto, Message = "更新成功" };
    }

    #region 查询
    /// <summary>
    /// 获取区域选择器
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult<List<AreaSelectDto>>> GetAreaSelectAsync()
    {
            var areas =  await _repository.Query().ToListAsync();
            var areaDtos =  _mapper.Map<List<AreaSelectDto>>(areas);       
            return new ApiResult<List<AreaSelectDto>> {Data = areaDtos, Message = "查询成功" };
    }

    /// <summary>
    /// 分页查询区域
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns></returns>
    public async Task<ApiResult<PageData<AreaSelectDto>>> GetAreaPageListAsync(QueryParameter queryParameter)
    {
            var result = _repository.Query().Skip((queryParameter.PageIndex - 1) * queryParameter.PageSize).
                             Take(queryParameter.PageSize);
            var dtos = _mapper.Map<List<AreaSelectDto>>(result);
            var totalCount = await _repository.CountAsync();
            PageData<AreaSelectDto> pageData = new ()
            {
                PageIndex = queryParameter.PageIndex,
                PageSize = queryParameter.PageSize,
                List = dtos,
                Total = totalCount
            };
            return new ApiResult<PageData<AreaSelectDto>> {Message = "查询成功",Data = pageData};        
    }
    #endregion
}
