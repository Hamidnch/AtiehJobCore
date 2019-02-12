using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Topics;
using AtiehJobCore.Services.Extensions;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Services.Seo;
using AtiehJobCore.Services.Topics;
using AtiehJobCore.Web.Framework.Infrastructure.Cache;
using AtiehJobCore.Web.Framework.Models.Topics;
using System;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial class TopicViewModelService : ITopicViewModelService
    {
        private readonly ITopicService _topicService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        //private readonly ITopicTemplateService _topicTemplateService;
        private readonly IAclService _aclService;

        public TopicViewModelService(ITopicService topicService,
            IWorkContext workContext,
            ICacheManager cacheManager,
            //ITopicTemplateService topicTemplateService,
            IAclService aclService)
        {
            this._topicService = topicService;
            this._workContext = workContext;
            this._cacheManager = cacheManager;
            //this._topicTemplateService = topicTemplateService;
            this._aclService = aclService;
        }
        public virtual TopicModel PrepareTopicModel(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            var model = new TopicModel
            {
                Id = topic.Id,
                SystemName = topic.SystemName,
                IncludeInSitemap = topic.IncludeInSitemap,
                IsPasswordProtected = topic.IsPasswordProtected,
                Title = topic.IsPasswordProtected ? "" : topic.GetLocalized(x => x.Title),
                Body = topic.IsPasswordProtected ? "" : topic.GetLocalized(x => x.Body),
                MetaKeywords = topic.GetLocalized(x => x.MetaKeywords),
                MetaDescription = topic.GetLocalized(x => x.MetaDescription),
                MetaTitle = topic.GetLocalized(x => x.MetaTitle),
                SeName = topic.GetSeName(),
                TopicTemplateId = topic.TopicTemplateId
            };
            return model;

        }
        public virtual TopicModel TopicDetails(string topicId)
        {

            var cacheKey = string.Format(ModelCacheEventConsumer.TopicModelByIdKey,
                topicId,
                _workContext.WorkingLanguage.Id,
                string.Join(",", _workContext.CurrentUser.GetUserRoleIds()));
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                var topic = _topicService.GetTopicById(topicId);
                if (topic == null)
                    return null;

                //ACL (access control list)
                return !_aclService.Authorize(topic) ? null : PrepareTopicModel(topic);
            }
            );

            return cacheModel;

        }
        public virtual TopicModel TopicDetailsPopup(string systemName)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.TopicModelBySystemNameKey,
                systemName,
                _workContext.WorkingLanguage.Id,
                string.Join(",", _workContext.CurrentUser.GetUserRoleIds()));

            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                //load by store
                var topic = _topicService.GetTopicBySystemName(systemName);
                if (topic == null)
                    return null;
                //ACL (access control list)
                return !_aclService.Authorize(topic) ? null : PrepareTopicModel(topic);
            });
            return cacheModel;
        }
        public virtual TopicModel TopicBlock(string systemName)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.TopicModelBySystemNameKey,
                systemName,
                _workContext.WorkingLanguage.Id,
                string.Join(",", _workContext.CurrentUser.GetUserRoleIds()));
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                //load by store
                var topic = _topicService.GetTopicBySystemName(systemName);
                if (topic == null)
                    return null;
                //ACL (access control list)
                return !_aclService.Authorize(topic) ? null : PrepareTopicModel(topic);
            });

            return cacheModel;
        }
        //public virtual string PrepareTopicTemplateViewPath(string templateId)
        //{
        //    var templateCacheKey = string.Format(ModelCacheEventConsumer.TOPIC_TEMPLATE_MODEL_KEY, templateId);
        //    var templateViewPath = _cacheManager.Get(templateCacheKey, () =>
        //    {
        //        var template = _topicTemplateService.GetTopicTemplateById(templateId);
        //        if (template == null)
        //            template = _topicTemplateService.GetAllTopicTemplates().FirstOrDefault();
        //        if (template == null)
        //            throw new Exception("No default template could be loaded");
        //        return template.ViewPath;
        //    });
        //    return templateViewPath;
        //}
    }
}
