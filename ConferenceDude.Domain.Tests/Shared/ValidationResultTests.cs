using System.Diagnostics.CodeAnalysis;
using ConferenceDude.Domain.Shared;
using NUnit.Framework;

namespace ConferenceDude.Domain.Tests.Shared
{
    [ExcludeFromCodeCoverage]
    public class ValidationResultTests
    {
        [Test]
        public void ValidationResult_without_Errors_should_be_successfully()
        {
            var sut = new ValidationResult();
            Assert.That(sut.Success, Is.True);
        }

        [Test]
        public void ValidationResult_with_one_Error_should_not_be_successfully()
        {
            var sut = new ValidationResult();
            sut.AddError("aField", "aMessage");
            Assert.That(sut.Success, Is.False);
        }
    }
}