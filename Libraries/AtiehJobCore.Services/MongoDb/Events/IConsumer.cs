namespace AtiehJobCore.Services.MongoDb.Events
{
    public interface IConsumer<in T>
    {
        void HandleEvent(T eventMessage);
    }
}
