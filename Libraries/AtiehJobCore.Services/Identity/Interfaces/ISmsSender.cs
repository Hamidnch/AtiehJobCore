using System.Threading.Tasks;

namespace AtiehJobCore.Services.Identity.Interfaces
{
    public interface ISmsSender
    {
        #region BaseClass

        Task SendSmsAsync(string number, string message);

        #endregion

        #region Methods

        #endregion
    }
}