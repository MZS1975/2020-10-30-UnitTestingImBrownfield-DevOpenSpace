namespace ConferenceDude.Domain.Tests.Unit.Session
{
    using System;
    using Domain.Session;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class SessionShould
    {
        [Fact]
        public void AllowIdentityAssignmentOnCreation()
        {
            var identity = new SessionIdentity(1);
            Action act = () => _ = new Session(identity, "someTitle", "someAbstract");
            act.Should().NotBeNull();
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void ReportIfNew(int id, bool isNew)
        {
            var identity = new SessionIdentity(id);
            var sut = new Session(identity, "someTitle", "someAbstract");
            sut.IsNew.Should().Be(isNew);
        }

        public class RequireATitle
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void AtCreation(string title)
            {
                Action act = () => _ = new Session(title, "someAbstract");

                act.Should().Throw<ModelException>().Where(e => e.PropertyName == "Title" && e.Message == "Title cannot be null or whitespace.");
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void AtChange(string title)
            {
                var sut = new Session("someTitle", "someAbstract");
                Action act = () => sut.ChangeTitle(title);


                act.Should().Throw<ModelException>().Where(e => e.PropertyName == "Title" && e.Message == "Title cannot be null or whitespace.");
            }
        }

        public class RequireAnAbstract
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void AtCreation(string @abstract)
            {
                Action act = () => _ = new Session("someTitle", @abstract);

                act.Should().Throw<ModelException>().Where(e => e.PropertyName == "Abstract" && e.Message == "Abstract cannot be null or whitespace.");
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void AtChange(string @abstract)
            {
                var sut = new Session("someTitle", "someAbstract");
                Action act = () => sut.ChangeAbstract(@abstract);


                act.Should().Throw<ModelException>().Where(e => e.PropertyName == "Abstract" && e.Message == "Abstract cannot be null or whitespace.");
            }
        }
    }
}
