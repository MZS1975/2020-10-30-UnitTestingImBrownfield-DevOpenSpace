namespace ConferenceDude.Application
{
    using Conference;
    using Domain.Session.Policies;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Session;

    public class ConferencePlanner
    {
        public ConferencePlanner(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(configuration, serviceCollection);

            Services = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider Services { get; }

        public IConferencePlanningService ConferencePlanning => Services.GetRequiredService<IConferencePlanningService>();

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<ISessionRestClient, SessionRestClient>();
            services.AddSingleton<ISessionTransportConverter, SessionTransportConverter>();
            services.AddSingleton<ISessionPolicyService, SessionPolicyService>();
            services.AddSingleton<ISessionPlanningService, SessionPlanningService>();
            services.AddSingleton<IConferencePlanningService, ConferencePlanningService>();
        }
    }
}
