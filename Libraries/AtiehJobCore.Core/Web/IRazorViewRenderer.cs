using System.Threading.Tasks;

namespace AtiehJobCore.Core.Web
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
