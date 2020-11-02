namespace ConferenceDude.Tests.Integration
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Xunit.Abstractions;

    public class LocalServerFixture : WebApplicationFactory<Server.Program>
    {
        // Must be set in each test
        public ITestOutputHelper Output { get; set; }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = base.CreateWebHostBuilder();
            builder.ConfigureLogging(logging =>
                                     {
                                         logging.ClearProviders(); // Remove other loggers
                                         logging.AddXUnit(Output); // Use the ITestOutputHelper instance
                                     });

            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Don't run IHostedServices when running as a test
            builder.ConfigureTestServices((services) =>
                                          {
                                              services.RemoveAll(typeof(IHostedService));
                                          });
        }
    }
}
