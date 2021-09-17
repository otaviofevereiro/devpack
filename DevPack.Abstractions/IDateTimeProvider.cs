namespace System
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        TimeZoneInfo TimeZone { get; }
        DateTime Today { get; }
        DateTime UtcNow { get; }
    }
}