using AutoMapper;
using Repair.Core.Extensions.Enum;
using Repair.Entity.Dto.SysDto.AppUser;
using Repair.Entity.Dto.SysDto.Area;
using Repair.Entity.Dto.SysDto.AuditLog;
using Repair.Entity.Dto.SysDto.Comment;
using Repair.Entity.Dto.SysDto.RepairOrder;
using Repair.Entity.SysEntity;

namespace Repair.Api.Configuration;

/// <summary>
/// AutoMapper配置
/// </summary>
public class AutoMapperConfig : Profile
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public AutoMapperConfig()
    {
        CreateMap<AppUserAddDto, AppUser>();
        CreateMap<AppUser, AppUserDto>();
        CreateMap<AppUserUpdateDto, AppUser>();
        CreateMap<AppUser, AppUserUpdateDto>();
        CreateMap<AppUser, RepairWorkerSelectDto>();

        CreateMap<AreaAddDto,Area>();
        CreateMap<AreaUpdateDto, Area>();
        CreateMap<Area, AreaUpdateDto>();
        CreateMap<Area, AreaSelectDto>();

        CreateMap<RepairOrderAddDto, RepairOrder>();
        CreateMap<RepairOrderUpdateDto, RepairOrder>();
        CreateMap<RepairOrder, RepairOrderUpdateDto>();
        CreateMap<RepairOrder, RepairOrderUserDisplayDto>()
          .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName))
          .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => EnumException.GetDescription(src.Status)))
          .ForMember(dest => dest.OrderTypeDescription, opt => opt.MapFrom(src => EnumException.GetDescription(src.OrderType)));
        CreateMap<RepairOrder, RepairOrderWorkerDisplayDto>()
          .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName))
          .ForMember(dest => dest.OrderTypeDescription, opt => opt.MapFrom(src => EnumException.GetDescription(src.OrderType)));
        CreateMap<RepairOrder, RepairOrderAssignmentDto>()
          .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName))
          .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => EnumException.GetDescription(src.Status)))
          .ForMember(dest => dest.OrderTypeDescription, opt => opt.MapFrom(src => EnumException.GetDescription(src.OrderType)));

        CreateMap<AuditLogAddDto, AuditLog>();
        CreateMap<AuditLogUpdateDto, AuditLog>();
        CreateMap<AuditLog, AuditLogUpdateDto>();

        CreateMap<CommentAddDto, Comment>();
        CreateMap<Comment, CommentDto>();

        
        
    }
}
