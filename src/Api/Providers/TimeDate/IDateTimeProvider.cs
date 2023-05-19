namespace Api.Providers.TimeDate
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
