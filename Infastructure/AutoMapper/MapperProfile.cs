using AutoMapper;
using Domain.Models.Entity_s;
using Domain.Models.Entity;

namespace Infastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<NotifTypeCreatDto,NotificationType>().ReverseMap();
        CreateMap<NotifTypeDto,NotificationType>().ReverseMap();
        CreateMap<UserNotifCreateDto,UserNotification>().ReverseMap();
        CreateMap<UserNotifDto,UserNotification>().ReverseMap();
    }
}