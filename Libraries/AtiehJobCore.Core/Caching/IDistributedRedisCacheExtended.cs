using System.Threading.Tasks;

namespace AtiehJobCore.Core.Caching
{
    public interface IDistributedRedisCacheExtended
    {
        Task ClearAsync();
        Task RemoveByPatternAsync(string pattern);
    }
}
