using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    public class RequestUserIdEnricher : ILogEventEnricher
    {
        private const string UserIdPropertyName = "RequestUserId";
        private readonly IHttpContextAccessor _contextAccessor;

        public RequestUserIdEnricher() : this(new HttpContextAccessor())
        {
        }

        public RequestUserIdEnricher(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_contextAccessor.HttpContext == null)
                return;

            var userId = _contextAccessor.HttpContext.User.GetUserId();

            var correlationIdProperty = new LogEventProperty(UserIdPropertyName, new ScalarValue(userId));

            logEvent.AddOrUpdateProperty(correlationIdProperty);
        }
    }
}