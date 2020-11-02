namespace ConferenceDude.Application.Tests.Unit
{
    using System.Collections.Generic;
    using Conference;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class ConferencePlannerShould
    {
        [Fact]
        public void WireUpPlanningSystem()
        {
            var settings = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("serverUri", "http://localhost:51262")
            };

            var configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(settings)
                                .Build();


            var sut = new ConferencePlanner(configuration);

            sut.ConferencePlanning.Should().NotBeNull().And.BeAssignableTo<IConferencePlanningService>();
        }
    }
}
