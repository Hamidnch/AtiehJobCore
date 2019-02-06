using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.Web.Framework.Models.Admin;
using AutoMapper;

namespace AtiehJobCore.Web.Areas.Admin.Infrastructure.Mapper
{
    public class AdminMapperSettingsConfiguration : Profile, IMapperProfile
    {
        public AdminMapperSettingsConfiguration()
        {
            CreateMap<UserSettings, UserSettingsModel.UserSettingModel>()
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            CreateMap<UserSettingsModel.UserSettingModel, UserSettings>()
                .ForMember(dest => dest.HashedPasswordFormat, mo => mo.Ignore())
                .ForMember(dest => dest.AvatarMaximumSizeBytes, mo => mo.Ignore())
                .ForMember(dest => dest.OnlineUserMinutes, mo => mo.Ignore())
                .ForMember(dest => dest.SuffixDeletedUsers, mo => mo.Ignore());
        }

        public int Order => 0;
    }
}
