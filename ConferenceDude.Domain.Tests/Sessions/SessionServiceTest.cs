using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDude.Domain.Sessions;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace ConferenceDude.Domain.Tests.Sessions
{
    [ExcludeFromCodeCoverage]
    public class SessionServiceTest
    {
        private InMemorySessionRepository _sessionRepository;
        private SessionService _sut;

        [SetUp]
        public void SetUp()
        {
            _sessionRepository = new InMemorySessionRepository();
            _sut = new SessionService(_sessionRepository);
        }

        [Test]
        public async Task SessionService_Create_should_add_new_Session_to_Repository()
        {
            var newSession = Builder<Session>.CreateNew().With(s => s.Id = 0).Build();

            var result = await _sut.Create(newSession);

            Assert.Multiple(() =>
            {
                Assert.That(result.validationResult.Success, Is.True);
                Assert.That(_sessionRepository.Sessions, Contains.Item(newSession));
                Assert.That(result.id, Is.EqualTo(newSession.Id));
            });
        }

        [Test]
        public async Task SessionService_Create_should_not_add_two_Sessions_with_same_name()
        {
            var uniqueSessionTitle = "Unique Session Title";
            var firstSession = Builder<Session>
                .CreateNew()
                .With(s => s.Title = uniqueSessionTitle)
                .And(s => s.Id = 1)
                .Build();
            var secondSession = Builder<Session>
                .CreateNew()
                .With(s => s.Title = uniqueSessionTitle)
                .And(s => s.Id = 2)
                .Build();
            _sessionRepository.Sessions.Add(firstSession);

            var result = await _sut.Create(secondSession);

            Assert.Multiple(() =>
            {
                Assert.That(result.validationResult.Success, Is.False);
                Assert.That(result.validationResult.Messages.Select(m => m.FieldName),
                    Contains.Item(nameof(Session.Title)));
                Assert.That(result.validationResult.Messages.First(m => m.FieldName == nameof(Session.Title)).ErrorMessage,
                    Is.EqualTo("Eine Session mit diesem Titel ist bereits vorhanden."));
                Assert.That(_sessionRepository.Sessions, Does.Not.Contain(secondSession));
            });
        }
    }
}