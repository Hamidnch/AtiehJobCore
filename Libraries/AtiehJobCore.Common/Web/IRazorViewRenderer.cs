using System.Threading.Tasks;

namespace AtiehJobCore.Common.Web
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
