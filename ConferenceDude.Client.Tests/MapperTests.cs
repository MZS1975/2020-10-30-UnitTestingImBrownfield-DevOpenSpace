using System.Diagnostics.CodeAnalysis;
using ConferenceDude.Domain.Sessions;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace ConferenceDude.Client.Tests
{
    [ExcludeFromCodeCoverage]
    public class MapperTests
    {
        [Test]
        public void SessionModel_ToSession_should_map_all_Properties()
        {
            var sessionModel = Builder<SessionModel>.CreateNew().Build();
            var session = sessionModel.ToSession();
            session.Should().BeEquivalentTo(sessionModel);
        }

        [Test]
        public void Session_ToSessionModel_should_map_all_Properties()
        {
            var session = Builder<Session>.CreateNew().Build();
            var sessionModel = session.ToSessionModel();
            sessionModel.Should().BeEquivalentTo(session);
        }
    }
}