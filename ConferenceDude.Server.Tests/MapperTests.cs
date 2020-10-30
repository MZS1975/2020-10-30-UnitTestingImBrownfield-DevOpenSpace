using System.Diagnostics.CodeAnalysis;
using ConferenceDude.Domain.Sessions;
using ConferenceDude.Server.Controllers;
using ConferenceDude.Server.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace ConferenceDude.Server.Tests
{
    [ExcludeFromCodeCoverage]
    public class MapperTests
    {
        [Test]
        public void SessionEntity_ToSession_should_map_all_Properties()
        {
            var sessionEntity = Builder<SessionEntity>.CreateNew().Build();
            var session = sessionEntity.ToSession();
            session.Should().BeEquivalentTo(sessionEntity);
        }

        [Test]
        public void Session_ToSessionEntity_should_map_all_Properties()
        {
            var session = Builder<Session>.CreateNew().Build();
            var sessionModel = session.ToSessionEntity();
            sessionModel.Should().BeEquivalentTo(session);
        }

        [Test]
        public void SessionDto_ToSession_should_map_all_Properties()
        {
            var sessionDto = Builder<SessionDto>.CreateNew().Build();
            var session = sessionDto.ToSession();
            session.Should().BeEquivalentTo(sessionDto);
        }

        [Test]
        public void Session_ToSessionDto_should_map_all_Properties()
        {
            var session = Builder<Session>.CreateNew().Build();
            var sessionDto = session.ToSessionDto();
            sessionDto.Should().BeEquivalentTo(session);
        }

    }
}