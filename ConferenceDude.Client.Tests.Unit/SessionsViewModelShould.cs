namespace ConferenceDude.UI.Tests.Unit
{
    using System.Collections.Generic;
    using Domain.Session;
    using FluentAssertions;
    using Xunit;

    public class SessionsViewModelShould
    {
        private readonly UserInteractionFake _userInteractionService;
        private readonly ConferencePlanningServiceFake _conferencePlanningService;
        private readonly SessionsViewModel _sut;

        public SessionsViewModelShould()
        {
            _userInteractionService = new UserInteractionFake();
            _conferencePlanningService = new ConferencePlanningServiceFake();
            _sut = new SessionsViewModel(_userInteractionService, _conferencePlanningService);
        }

        [Fact]
        public void ConfirmSuccessfulSessionCreation()
        {
            _sut.CurrentSession = new SessionVewModel
            {
                Title = "Foo",
                Abstract = "Bar"
            };

            _sut.SaveSessionCommand.Execute(null);

            _userInteractionService.Errors.Should().BeEmpty();
            _userInteractionService.Messages.Should().ContainMatch("* successfully saved.");
        }

        [Fact]
        public void NotifyOnSessionCreationError()
        {
            IEnumerable<Session> sessions = new List<Session>
            {
                new Session(new SessionIdentity(1), "Foo", "Bar")
            };
            _conferencePlanningService.Setup(sessions);
            _sut.CurrentSession = new SessionVewModel
            {
                Title = "Foo",
                Abstract = "Bar"
            };

            _sut.SaveSessionCommand.Execute(null);

            _userInteractionService.Messages.Should().BeEmpty();
            _userInteractionService.Errors.Should().ContainMatch("* Title * is not unique.");
        }

        [Fact]
        public void ConfirmSuccessfulSessionChange()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            IEnumerable<Session> sessions = new List<Session>
            {
                session
            };
            _conferencePlanningService.Setup(sessions);
            _sut.CurrentSession = new SessionVewModel(session)
            {
                Abstract = "Bar Bar"
            };

            _sut.SaveSessionCommand.Execute(null);

            _userInteractionService.Errors.Should().BeEmpty();
            _userInteractionService.Messages.Should().ContainMatch("* successfully saved.");
        }

        [Fact]
        public void NotifyOnSessionChangeError()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            IEnumerable<Session> sessions = new List<Session>
            {
                session,
                new Session(new SessionIdentity(2), "Foo2", "Bar")
            };
            _conferencePlanningService.Setup(sessions);
            _sut.CurrentSession = new SessionVewModel(session)
            {
                Title = "Foo2"
            };

            _sut.SaveSessionCommand.Execute(null);

            _userInteractionService.Messages.Should().BeEmpty();
            _userInteractionService.Errors.Should().ContainMatch("* Title * is not unique.");
        }

        [Fact]
        public void NotifyOnSessionChangeException()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            IEnumerable<Session> sessions = new List<Session>
            {
                session,
                new Session(new SessionIdentity(2), "Foo2", "Bar")
            };
            _conferencePlanningService.Setup(sessions, true);
            _sut.CurrentSession = new SessionVewModel(session)
            {
                Title = "Foo2"
            };

            _sut.SaveSessionCommand.Execute(null);

            _userInteractionService.Messages.Should().BeEmpty();
            _userInteractionService.Errors.Should().ContainMatch("* could not be saved.");
        }

        [Fact]
        public void QueryBeforeSessionRemoval()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            IEnumerable<Session> sessions = new List<Session>
            {
                session,
                new Session(new SessionIdentity(2), "Foo2", "Bar")
            };
            _conferencePlanningService.Setup(sessions);
            _sut.CurrentSession = new SessionVewModel(session);
            _userInteractionService.ConfigureYesNo(false);

            _sut.RemoveSessionCommand.Execute(null);

            _conferencePlanningService.Sessions.Should().HaveCount(2);
        }

        [Fact]
        public void NotifyOnSessionRemovalException()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            IEnumerable<Session> sessions = new List<Session>
            {
                session,
                new Session(new SessionIdentity(2), "Foo2", "Bar")
            };
            _conferencePlanningService.Setup(sessions, false, true);
            _sut.CurrentSession = new SessionVewModel(session);
            _userInteractionService.ConfigureYesNo(true);

            _sut.RemoveSessionCommand.Execute(null);

            _userInteractionService.Messages.Should().BeEmpty();
            _userInteractionService.Errors.Should().ContainMatch("* could not be removed.");
        }

        [Fact]
        public void ConfirmSuccessfulSessionRemoval()
        {
            var session = new Session(new SessionIdentity(1), "Foo", "Bar");
            IEnumerable<Session> sessions = new List<Session>
            {
                session,
                new Session(new SessionIdentity(2), "Foo2", "Bar")
            };
            _conferencePlanningService.Setup(sessions);
            _sut.CurrentSession = new SessionVewModel(session);
            _userInteractionService.ConfigureYesNo(true);

            _sut.RemoveSessionCommand.Execute(null);

            _userInteractionService.Messages.Should().ContainMatch("* successfully removed.");
            _userInteractionService.Errors.Should().BeEmpty();
        }
    }
}
