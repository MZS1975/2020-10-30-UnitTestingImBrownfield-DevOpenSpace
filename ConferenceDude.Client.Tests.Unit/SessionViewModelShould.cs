namespace ConferenceDude.UI.Tests.Unit
{
    using Domain.Session;
    using FluentAssertions;
    using FluentAssertions.Events;
    using Xunit;

    public class SessionViewModelShould
    {
        private readonly SessionVewModel _sut;

        public SessionViewModelShould()
        {
            _sut = new SessionVewModel();
        }

        [Fact]
        public void BeInitializeFromSessionEntity()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            var sut = new SessionVewModel(session);
            sut.Id.Should().Be(session.Id);
            sut.Title.Should().Be(session.Title);
            sut.Abstract.Should().Be(session.Abstract);
        }

        [Fact]
        public void BeValidWithValidSession()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            var sut = new SessionVewModel(session);
            sut.HasErrors.Should().BeFalse();
        }

        [Fact]
        public void HaveTitleMissingErrorWithNewSession()
        {
            _sut.GetErrors(nameof(_sut.Title)).Should().NotBeEmpty();
        }

        [Fact]
        public void ClearTitleMissingErrorWithValidTitle()
        {
            _sut.Title = "Foo";

            _sut.GetErrors(nameof(_sut.Title)).Should().BeEmpty();
            _sut.GetErrors(nameof(_sut.Abstract)).Should().NotBeEmpty();
        }

        [Fact]
        public void NotifyOnTitleChange()
        {
            using IMonitor<SessionVewModel> monitor = _sut.Monitor();
            _sut.Title = "Foo";
            monitor.Should().RaisePropertyChangeFor(m => m.Title);
            monitor.Clear();
            _sut.Title = "Foo";
            monitor.Should().NotRaisePropertyChangeFor(m => m.Title);
        }

        [Fact]
        public void HaveAbstractMissingErrorWithNewSession()
        {
            _sut.GetErrors(nameof(_sut.Abstract)).Should().NotBeEmpty();
        }

        [Fact]
        public void ClearAbstractMissingErrorWithValidAbstract()
        {
            _sut.Abstract = "Bar";

            _sut.GetErrors(nameof(_sut.Title)).Should().NotBeEmpty();
            _sut.GetErrors(nameof(_sut.Abstract)).Should().BeEmpty();
        }

        [Fact]
        public void ReturnSessionEntityFromData()
        {
            _sut.Title = "Foo";
            _sut.Abstract = "Bar";

            var session = _sut.ToSession();
            session.Id.Should().Be((SessionIdentity)_sut.Id);
            session.Title.Should().Be(_sut.Title);
            session.Abstract.Should().Be(_sut.Abstract);
        }

        [Fact]
        public void NotifyOnAbstractChange()
        {
            using IMonitor<SessionVewModel> monitor = _sut.Monitor();
            _sut.Abstract = "Bar";
            monitor.Should().RaisePropertyChangeFor(m => m.Abstract);
            monitor.Clear();
            _sut.Abstract = "Bar";
            monitor.Should().NotRaisePropertyChangeFor(m => m.Abstract);
        }
    }
}
