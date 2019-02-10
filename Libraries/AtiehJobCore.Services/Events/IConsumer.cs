namespace AtiehJobCore.Services.Events
{
    public interface IConsumer<in T>
    {
        void HandleEvent(T eventMessage);
    }
}
