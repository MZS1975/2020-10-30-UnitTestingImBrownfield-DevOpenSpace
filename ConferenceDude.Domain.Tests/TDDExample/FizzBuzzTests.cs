using ConferenceDude.Domain.TDDExample;
using NUnit.Framework;

namespace ConferenceDude.Domain.Tests.TDDExample
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(4, "4")]
        [TestCase(98, "98")]
        public void FizzBuzz_mit_natuerlichen_Zahlen(int zahl, string expected)
        {
            var sut = new FizzBuzz();
            var actual = sut.ToFizzBuzz(zahl);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("3", "Fizz")]
        [TestCase("6", "Fizz")]
        [TestCase("9", "Fizz")]
        [TestCase("99", "Fizz")]
        public void FizzBuz_mit_Zahl_durch_drei_teilbar(int zahl, string expected)
        {
            var sut = new FizzBuzz();
            var actual = sut.ToFizzBuzz(zahl);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(5, "Buzz")]
        [TestCase(10, "Buzz")]
        [TestCase(20, "Buzz")]
        public void FizzBuzz_mit_Zahl_durch_fuenf_teilbar(int zahl, string expected)
        {
            var sut = new FizzBuzz();
            var actual = sut.ToFizzBuzz(zahl);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(15, "FizzBuzz")]
        [TestCase(30, "FizzBuzz")]
        public void FizzBuzz_mit_Zahl_druch_drei_und_fuenf_teilbar(int zahl, string expected)
        {
            var sut = new FizzBuzz();
            var actual = sut.ToFizzBuzz(zahl);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}