using AutoMapper;
using Jeton.Admin.Web.Models;
using Jeton.Admin.Web.ViewModel;
using Jeton.Core.Entities;

namespace Jeton.Admin.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(Config);
        }

        private static void Config(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<App, AppModel>().ReverseMap();
            cfg.CreateMap<App, AppViewModel>().ReverseMap();
            cfg.CreateMap<Setting, SettingModel>().ReverseMap();
            cfg.CreateMap<Token, TokenModel>().ReverseMap();
            cfg.CreateMap<Setting, SettingViewModel>().ReverseMap();
            cfg.CreateMap<User, UserModel>().ReverseMap();
            cfg.CreateMap<Log, LogModel>();
        }
    }
}