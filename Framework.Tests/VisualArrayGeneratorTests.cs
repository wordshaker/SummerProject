using Framework.Utilities;
using Framework.VisualArray;
using Moq;
using NUnit.Framework;

namespace Framework.Tests
{
    [TestFixture]
    public class VisualArrayGeneratorTests
    {
        private int[] _result;
        private Mock<IRandomNumberProvider> _randomNumberGenerator;

        [TestFixtureSetUp]
        public void WhenGeneratingAVisualArray()
        {
            _randomNumberGenerator = new Mock<IRandomNumberProvider>();
            _randomNumberGenerator
                .Setup(r => r.Take())
                .Returns(3);

            var array = new VisualArrayGenerator(_randomNumberGenerator.Object);
            _result = array.Generate();
        }

        [Test]
        public void ThenRandomNumberProviderWasCalled()
        {
            _randomNumberGenerator.Verify(r => r.Take());
        }

        [Test]
        public void ThenTheArrayContainsSevenMembers()
        {
            Assert.That(_result.Length, Is.EqualTo(7));
        }

        [Test]
        public void ThenTheFifthMemberOfTheArrayIsZero()
        {
            Assert.That(_result[4], Is.EqualTo(0));
        }

        [Test]
        public void ThenTheFirstMemberOfTheArrayIsZero()
        {
            Assert.That(_result[0], Is.EqualTo(0));
        }

        [Test]
        public void ThenTheFourthMemberOfTheArrayIsOne()
        {
            Assert.That(_result[3], Is.EqualTo(1));
        }

        [Test]
        public void ThenTheSecondMemberOfTheArrayIsZero()
        {
            Assert.That(_result[1], Is.EqualTo(0));
        }

        [Test]
        public void ThenTheSeventhMemberOfTheArrayIsZero()
        {
            Assert.That(_result[6], Is.EqualTo(0));
        }

        [Test]
        public void ThenTheSixthMemberOfTheArrayIsZero()
        {
            Assert.That(_result[5], Is.EqualTo(0));
        }

        [Test]
        public void ThenTheThirdMemberOfTheArrayIsZero()
        {
            Assert.That(_result[2], Is.EqualTo(0));
        }
    }
}