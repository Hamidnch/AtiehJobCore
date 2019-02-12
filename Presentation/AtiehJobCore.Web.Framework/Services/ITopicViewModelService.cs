using AtiehJobCore.Core.Domain.Topics;
using AtiehJobCore.Web.Framework.Models.Topics;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial interface ITopicViewModelService
    {
        TopicModel PrepareTopicModel(Topic topic);
        TopicModel TopicDetails(string topicId);
        TopicModel TopicDetailsPopup(string systemName);
        TopicModel TopicBlock(string systemName);
        //string PrepareTopicTemplateViewPath(string templateId);
    }
}
