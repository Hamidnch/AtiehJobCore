using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Web.Framework.Models.Admin;
using AutoMapper;

namespace AtiehJobCore.Web.Areas.Admin.Infrastructure.Mapper
{
    public class AdminMapperModelConfiguration : Profile, IMapperProfile
    {
        public AdminMapperModelConfiguration()
        {
            //language
            CreateMap<Language, AdminLanguageModel>()
                .ForMember(dest => dest.FlagFileNames, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            CreateMap<AdminLanguageModel, Language>()
                .ForMember(dest => dest.Id, mo => mo.Ignore());
        }

        public int Order => 0;
    }
}
