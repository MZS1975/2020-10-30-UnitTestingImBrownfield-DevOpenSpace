namespace ConferenceDude.Domain.Tests.Unit.Session.Policies
{
    using System.Collections.Generic;
    using Domain.Session;
    using Domain.Session.Policies;
    using FluentAssertions;
    using Xunit;

    public class UniqueTitlePolicyShould
    {
        [Theory]
        [MemberData(nameof(TestCases))]
        public void ReportDuplicateTitles(IEnumerable<Session> previousSessions, Session sessionToTest, bool expectedOutcome)
        {
            var sut = new UniqueTitlePolicy();

            sut.Check(previousSessions, sessionToTest).Should().Be(expectedOutcome);
        }

        public static IEnumerable<object[]> TestCases
        {
            get
            {
                yield return new object[] // add first unsaved session
                {
                    new List<Session>(),
                    new Session("TC1", "Foo"), 
                    true
                };
                yield return new object[] // add two unsaved sessions with same title
                {
                    new List<Session> { new Session("TC1", "Foo") },
                    new Session("TC1", "Foo"),
                    false
                };
                yield return new object[] // add two unsaved sessions with different titles
                {
                    new List<Session> { new Session("TC1", "Foo") },
                    new Session("TC2", "Foo"),
                    true
                };
                yield return new object[] // alter title of saved session without conflict
                {
                    new List<Session> { new Session(new SessionIdentity(1), "TC1", "Foo") },
                    new Session(new SessionIdentity(1), "TC2", "Foo"),
                    true
                };
                yield return new object[] // alter title of saved session with conflict - variant 1
                {
                    new List<Session> { new Session(new SessionIdentity(1), "TC1", "Foo") },
                    new Session(new SessionIdentity(2), "TC1", "Foo"),
                    false
                };
                yield return new object[] // alter title of saved session with conflict - variant 2
                {
                    new List<Session> { new Session(new SessionIdentity(1), "TC1", "Foo"), new Session(new SessionIdentity(2), "TC2", "Foo") },
                    new Session(new SessionIdentity(2), "TC1", "Foo"),
                    false
                };
                yield return new object[] // add new unsaved session with conflict
                {
                    new List<Session> { new Session(new SessionIdentity(1), "TC1", "Foo") },
                    new Session( "TC1", "Foo"),
                    false
                };
            }
        }
    }
}
