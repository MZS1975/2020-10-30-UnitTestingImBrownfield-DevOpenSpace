namespace ConferenceDude.Tests.Integration
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Conference;
    using Application.Session;
    using Domain;
    using Domain.Session;
    using Domain.Session.Policies;
    using FluentAssertions;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class SessionPlanningSteps
    {
        private readonly ScenarioContext _context;
        private readonly ISessionPlanningService _planner;

        public SessionPlanningSteps(ScenarioContext context)
        {
            _context = context;
            _planner = _context.ScenarioContainer.Resolve<IConferencePlanningService>().SessionPlanner;

        }

        [Given(@"I have an empty  planning system")]
        public async Task GivenIHaveAnEmptyPlanningSystem()
        {
            await ClearDatabase();
        }

        [Given(@"I have the following planned sessions")]
        public async Task GivenIHaveTheFollowingPlannedSessions(Table table)
        {
            await ClearDatabase();
            var sessions = table.CreateSet<Session>(row => new Session(new SessionIdentity(Convert.ToInt32(row["Id"])), row["Title"], row["Abstract"]));
            foreach (Session session in sessions)
            {
                await _planner.StoreSessionAsync(session);
            }
        }


        [When(@"I add the following sessions")]
        public async Task WhenIAddTheFollowingSessions(Table table)
        {
            var sessions = table.CreateSet<Session>(row => new Session(new SessionIdentity(Convert.ToInt32(row["Id"])), row["Title"], row["Abstract"]));
            foreach (Session session in sessions)
            {
                await _planner.StoreSessionAsync(session);
            }
        }

        [When(@"I remove the following sessions")]
        public async Task WhenIRemoveTheFollowingSessions(Table table)
        {
            var sessions = await _planner.ListSessionsAsync();

            var sessionsToRemove = table.CreateSet<Session>(row => new Session(new SessionIdentity(0), row["Title"], row["Abstract"]));
            foreach (Session session in sessionsToRemove)
            {
                var realId = sessions.First(s => s.Title == session.Title).Id;
                await _planner.RemoveSessionAsync(realId);
            }
        }

        [When(@"I change the title of the following sessions")]
        public async Task WhenIChangeTheTitleOfTheFollowingSessions(Table table)
        {
            var sessions = await _planner.ListSessionsAsync();
            var updates = table.CreateSet<(string OldTitle, string NewTitle)>(row => ( row[0], row[1]));

            foreach ((string oldTitle, string newTitle) in updates)
            {
                var session = sessions.First(s => s.Title == oldTitle);
                session.ChangeTitle(newTitle);
                var result = await _planner.StoreSessionAsync(session);

                _context.Add("result", result);
            }
        }

        [When(@"I change the abstract of the following sessions")]
        public async Task WhenIChangeTheAbstractOfTheFollowingSessions(Table table)
        {
            var sessions = await _planner.ListSessionsAsync();
            var updates = table.CreateSet<(string Title, string NewAbstract)>(row => (row[0], row[1]));

            foreach ((string title, string newAbstract) in updates)
            {
                var session = sessions.First(s => s.Title == title);
                session.ChangeAbstract(newAbstract);
                await _planner.StoreSessionAsync(session);
            }
        }

        [Then(@"The planning system contains the following sessions")]
        public async Task ThenThePlanningSystemContainsTheFollowingSessions(Table table)
        {
            var expected = table.CreateSet<Session>(row => new Session(new SessionIdentity(Convert.ToInt32(row["Id"])), row["Title"], row["Abstract"]));
            var sessions = await _planner.ListSessionsAsync();
            sessions.Should().BeEquivalentTo(expected, o => o.Excluding(e => e.Id));
        }

        [Then(@"I receive a conflict")]
        public void ThenIReceiveAConflict(Table table)
        {
            var violations = table.CreateSet(row => Enum.Parse<SessionPolicy>(row[0]));
            var expected = new PolicyCheckResult<SessionPolicy>(violations);

            PolicyCheckResult<SessionPolicy> result = (PolicyCheckResult<SessionPolicy>)_context["result"];
            result.IsValid.Should().Be(expected.IsValid);
            result.Violations.Should().BeEquivalentTo(expected.Violations);
        }


        private async Task ClearDatabase()
        {
            var sessions = await _planner.ListSessionsAsync();
            foreach (Session session in sessions)
            {
                await _planner.RemoveSessionAsync(session.Id);
            }
        }
    }
}
