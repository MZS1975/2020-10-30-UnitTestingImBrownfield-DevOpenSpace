namespace ConferenceDude.Domain.Tests.Unit.Session.Policies
{
    using System.Collections.Generic;
    using Domain.Session;
    using Domain.Session.Policies;
    using FluentAssertions;
    using Xunit;

    public class SessionPolicyServiceShould
    {
        [Fact]
        public void EvaluatePoliciesOnDemand()
        {
            var sut = new SessionPolicyService();

            var storedSessions = new List<Session>
            {
                new Session(new SessionIdentity(1), "TC1", "Foo"),
                new Session(new SessionIdentity(2), "TC2", "Foo")
            };
            
            var changedSession = new Session(new SessionIdentity(2), "TC1", "Foo");

            var result = sut.VerifySession(storedSessions, changedSession);

            result.IsValid.Should().BeFalse();
            result.Violations.Should().Contain(SessionPolicy.UniqueTitle);
        }
    }
}
