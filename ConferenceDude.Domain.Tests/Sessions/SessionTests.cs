using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ConferenceDude.Domain.Sessions;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace ConferenceDude.Domain.Tests.Sessions
{
    [ExcludeFromCodeCoverage]
    public class SessionTests
    {
        [Test]
        public void Session_Validate_should_validate_a_valid_Session()
        {
            var sut = CreateValidSession();
            var validationResult = sut.Validate();
            Assert.That(validationResult.Success, Is.True);
        }

        [Test]
        public void Session_Validate_should_detect_empty_Title()
        {
            var sut = CreateValidSession();
            sut.Title = string.Empty;

            var validationResult = sut.Validate();

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Success,
                    Is.False);
                Assert.That(validationResult.Messages.Select(m => m.FieldName),
                    Contains.Item(nameof(Session.Title)));
                Assert.That(validationResult.Messages.First(m => m.FieldName == nameof(Session.Title)).ErrorMessage,
                    Is.EqualTo("Das Feld ist ein Pflichtfeld"));
            });
        }

        [Test]
        public void Session_Validate_should_detect_empty_Abstract()
        {
            var sut = CreateValidSession();
            sut.Abstract = string.Empty;

            var validationResult = sut.Validate();

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Success,
                    Is.False);
                Assert.That(validationResult.Messages.Select(m => m.FieldName),
                    Contains.Item(nameof(Session.Abstract)));
                Assert.That(validationResult.Messages.First(m => m.FieldName == nameof(Session.Abstract)).ErrorMessage,
                    Is.EqualTo("Das Feld ist ein Pflichtfeld"));
            });
        }

        [Test]
        public void Session_IsNew_after_new_should_return_true()
        {
            var sut = new Session();
            Assert.That(sut.IsNew(), Is.True);
        }

        [Test]
        public void Session_IsNew_after_a_load_should_return_false()
        {
            var sut = Builder<Session>.CreateNew().With(s => s.Id = 1).Build();
            Assert.That(sut.IsNew(), Is.False);
        }

        [Test]
        public void IsNew_with_new_Session()
        {
            var sut = CreateValidSession();
            sut.Id = 0;
            Assert.That(sut.IsNew, Is.True);
        }

        [Test]
        public void IsNew_with_existing_Session()
        {
            var sut = CreateValidSession();
            sut.Id = 73;
            Assert.That(sut.IsNew, Is.False);
        }

        private Session CreateValidSession()
        {
            return new Session
            {
                Title = "A Session Title",
                Abstract = "An Abstract"
            };
        }
    }
}