namespace AtiehJobCore.Core.Localization.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
