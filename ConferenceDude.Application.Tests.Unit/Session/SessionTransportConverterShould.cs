namespace ConferenceDude.Application.Tests.Unit.Session
{
    using Application.Session;
    using Domain.Session;
    using FluentAssertions;
    using Xunit;

    public class SessionTransportConverterShould
    {
        private SessionTransportConverter _sut;

        public SessionTransportConverterShould()
        {
            _sut = new SessionTransportConverter();
        }

        [Fact]
        public void CreateDtoFromSessionEntity()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            var expected = new SessionDto(1, "Foo", "Bar");

            var sessionDto = _sut.FromSession(session);


            sessionDto.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CreateSessionEntityFromDto()
        {
            var sessionDto = new SessionDto(1, "Foo", "Bar");
            var expected = new Session(new SessionIdentity(1), "Foo", "Bar");

            var session = _sut.ToSession(sessionDto);


            session.Should().BeEquivalentTo(expected);
        }
    }
}
