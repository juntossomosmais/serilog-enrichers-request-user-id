using Serilog.Core;
using Serilog.Events;
using System;

namespace Serilog.Enrichers.RequestUserId.Tests.Support
{
    public class DelegateSink : ILogEventSink
    {
        readonly Action<LogEvent> _write;

        public DelegateSink(Action<LogEvent> write)
        {
            _write = write ?? throw new ArgumentNullException(nameof(write));
        }

        public void Emit(LogEvent logEvent)
        {
            _write(logEvent);
        }
    }
}