namespace ConferenceDude.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using Application;
    using Application.Conference;
    using Application.Session;
    using Domain.Session.Policies;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;
    using Xunit;
    using Xunit.Abstractions;

    [Binding]
    public sealed class Setup : IClassFixture<LocalServerFixture>
    {
        private readonly LocalServerFixture _localServer;

        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public Setup(LocalServerFixture localServer)
        {
            _localServer = localServer;
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            _localServer.Output = context.ScenarioContainer.Resolve<ITestOutputHelper>();

            var settings = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("serverUri", "http://localhost:51262")
            };

            var configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(settings)
                                .Build();

            ConfigureServices(configuration, context);
        }

        private void ConfigureServices(IConfiguration configuration, ScenarioContext context)
        {
            context.ScenarioContainer.RegisterInstanceAs(configuration);
            WebApplicationFactoryClientOptions options = new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:51262"),
            };
            context.ScenarioContainer.RegisterInstanceAs(_localServer.CreateClient(options));
            context.ScenarioContainer.RegisterTypeAs<SessionRestClient, ISessionRestClient>();
            context.ScenarioContainer.RegisterTypeAs<SessionTransportConverter, ISessionTransportConverter>();
            context.ScenarioContainer.RegisterTypeAs<SessionPolicyService, ISessionPolicyService>();
            context.ScenarioContainer.RegisterTypeAs<SessionPlanningService, ISessionPlanningService>();
            context.ScenarioContainer.RegisterTypeAs<ConferencePlanningService, IConferencePlanningService>();
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext context)
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
