﻿using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Web.Framework.Models.Admin;

namespace AtiehJobCore.Web.Areas.Admin.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #region Languages

        public static AdminLanguageModel ToModel(this Language entity)
        {
            return entity.MapTo<Language, AdminLanguageModel>();
        }

        public static Language ToEntity(this AdminLanguageModel model)
        {
            return model.MapTo<AdminLanguageModel, Language>();
        }

        public static Language ToEntity(this AdminLanguageModel model, Language destination)
        {
            return model.MapTo(destination);
        }

        #endregion
    }
}
