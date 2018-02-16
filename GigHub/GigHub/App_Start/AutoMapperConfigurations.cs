using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub
{
    public static class AutoMapperConfigurations
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
        }
    }
}