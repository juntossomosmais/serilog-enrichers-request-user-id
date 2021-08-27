using FluentAssertions;
using Serilog.Configuration;
using Serilog.Enrichers.RequestUserId.Tests.Support;
using System;
using Xunit;

namespace Serilog.Enrichers.RequestUserId.Tests
{
    public class RequestUserIdLoggerConfigurationExtensionsTests
    {
        [Fact(DisplayName = "Should not throw exception when logging")]
        public void WithRequestUserId_Log_ShouldNotThrowException()
        {
            // Arrange & act
            var logger = new LoggerConfiguration()
                .Enrich.WithRequestUserId()
                .WriteTo.Sink(new DelegateSink(e => { }))
                .CreateLogger();
            Action act = () => logger.Information("LOG");

            // Assert
            act.Should().NotThrow();
        }

        [Fact(DisplayName = "Should throw exception if configuration is null")]
        public void WithRequestUserId_ConfigurationNull_ThrowException()
        {
            // Arrange & act
            LoggerEnrichmentConfiguration configuration = null;
            Action act = () => configuration.WithRequestUserId();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}