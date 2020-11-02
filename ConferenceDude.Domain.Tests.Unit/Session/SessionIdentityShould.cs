namespace ConferenceDude.Domain.Tests.Unit.Session
{
    using Domain.Session;
    using FluentAssertions;
    using Xunit;

    public class SessionIdentityShould
    {
        [Fact]
        public void BeIdentity()
        {
            var identityA = new SessionIdentity(1);

            (identityA == identityA).Should().Be(true);
            (identityA != identityA).Should().Be(false);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        public void BeCommutative(int a, int b)
        {
            var identityA = new SessionIdentity(a);
            var identityB = new SessionIdentity(b);

            (identityA == identityB).Should().Be(identityB == identityA);
            (identityA != identityB).Should().Be(identityB != identityA);
        }

        [Fact]
        public void BeCompatibleToTheirIntegerCounterparts()
        {
            var a = 1;
            var identityA = new SessionIdentity(a);

            identityA.Should().BeEquivalentTo((SessionIdentity)a);
            a.Should().Be(identityA);
            identityA.GetHashCode().Should().Be(a.GetHashCode());
            (identityA == a).Should().Be(a == identityA);
            (identityA != a).Should().Be(a != identityA);
        }
    }
}
