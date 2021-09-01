using Serilog.Core;
using Serilog.Events;
using System;

namespace Serilog.Enrichers.RequestUserId.Tests.Support
{
    /// <summary>
    /// Serilog Sink that calls the provided delegate with the log output.
    /// Can be used to capture log events and use it on assertions.
    /// </summary>
    public class DelegateSink : ILogEventSink
    {
        private readonly Action<LogEvent> _write;

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