using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class Configuration
    {
        public TimeSpan DateTimeOffset { get; private set; }

        public Configuration WithDateTimeOffSet(TimeSpan offset)
        {
            DateTimeOffset = offset;

            return this;
        }
    }
}
