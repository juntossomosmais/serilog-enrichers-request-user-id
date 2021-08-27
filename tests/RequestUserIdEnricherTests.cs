using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Serilog.Enrichers.RequestUserId.Tests.Support;
using Serilog.Events;
using System.Security.Claims;
using Xunit;

namespace Serilog.Enrichers.RequestUserId.Tests
{
    public class RequestUserIdEnricherTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly RequestUserIdEnricher _enricher;

        public RequestUserIdEnricherTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _enricher = new RequestUserIdEnricher(_mockHttpContextAccessor.Object);
        }

        [Fact(DisplayName = "Should not enrich log if HttpContext is null")]
        public void LogInformation_NullContext_DontEnrich()
        {
            // Assert
            LogEvent logEvent = null;
            var log = new LoggerConfiguration()
                .Enrich.With(_enricher)
                .WriteTo.Sink(new DelegateSink(e => logEvent = e))
                .CreateLogger();

            // Act
            log.Information(@"Test logging");

            // Assert
            logEvent.Properties.ContainsKey("RequestUserId").Should().BeFalse();
        }

        [Fact(DisplayName = "Should enrich log with empty property if claims identity is empty")]
        public void LogInformation_EmptyClaimsPrincipal_AddEmpty()
        {
            // Assert
            _mockHttpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext());

            LogEvent logEvent = null;
            var log = new LoggerConfiguration()
                .Enrich.With(_enricher)
                .WriteTo.Sink(new DelegateSink(e => logEvent = e))
                .CreateLogger();

            // Act
            log.Information(@"Test logging");

            // Assert
            logEvent.Properties.ContainsKey("RequestUserId").Should().BeTrue();
            var requestUserId = ((ScalarValue) logEvent.Properties["RequestUserId"]).Value;
            requestUserId.Should().Be("");
        }

        [Theory(DisplayName = "Should enrich log if user id or client id Claims is present")]
        [InlineData("uid", "UserId")]
        [InlineData("cid", "UserId")]
        public void LogInformation_EmptyClaimsPrincipal_AddUserId(string key, string value)
        {
            // Assert
            var context = new DefaultHttpContext();
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(key, value)
                })
            );
            _mockHttpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(context);

            LogEvent logEvent = null;
            var log = new LoggerConfiguration()
                .Enrich.With(_enricher)
                .WriteTo.Sink(new DelegateSink(e => logEvent = e))
                .CreateLogger();

            // Act
            log.Information(@"Test logging");

            // Assert
            logEvent.Properties.ContainsKey("RequestUserId").Should().BeTrue();
            var requestUserId = ((ScalarValue) logEvent.Properties["RequestUserId"]).Value;
            requestUserId.Should().Be(value);
        }
    }
}