namespace System
{
    /// <summary>
    /// Provide Date and Time configuring the offset of time zone
    /// </summary>
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        private readonly TimeSpan _offset;

        /// <summary>
        /// Create DateTimeProvider with default -03:00 offset
        /// </summary>
        public DateTimeProvider()
        { }

        /// <summary>
        /// Create DateTimeProvider with custom time offset
        /// </summary>
        /// <param name="offset">Custom time offset</param>
        public DateTimeProvider(TimeSpan offset)
        {
            _offset = offset;
            TimeZone = GetCustomTimeZoneInfo();
        }

        /// <summary>
        /// Gets a System.DateTime object that is set to the current date and time on this computer, expressed as the configured offset
        /// An object whose value is the current local date and time.
        /// </summary>
        public DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(UtcNow, TimeZone);

        /// <summary>
        /// Get a Custom Time Zone with configured offset.
        /// </summary>
        public TimeZoneInfo TimeZone { get; }

        /// <summary>
        /// Gets the date component of this instance.
        /// A new object with the same date as this instance, and the time value set to 12:00:00 midnight (00:00:00).
        /// </summary>
        public DateTime Today => Now.Date;

        /// <summary>
        /// Gets a System.DateTime object that is set to the current date and time on this
        /// computer, expressed as the Coordinated Universal Time (UTC).
        /// An object whose value is the current UTC date and time.
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Create a Custom Time Zone with configured offset.
        /// </summary>
        /// <returns>
        /// Return a custom Time Time Zone 
        /// </returns>
        /// <exception cref="InvalidCastException">Thow InvalidCastException when is a invalid offset is passed</exception>
        private TimeZoneInfo GetCustomTimeZoneInfo()
        {
            if (_offset == default)
                return TimeZoneInfo.Local;

            return TimeZoneInfo.CreateCustomTimeZone($"Time Zone", _offset, $"UTC({_offset}) Time Zone", "Time Zone");
        }
    }
}
