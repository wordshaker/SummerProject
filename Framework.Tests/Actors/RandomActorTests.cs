using Framework.Actors;
using Framework.Utilities;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Actors
{
    [TestFixture]
    public class RandomActorTests
    {
        private Mock<IRandomNumberProvider> _randomNumberGenerator;
        private int _actorsFixationPosition;
        private int _randomNumber;

        [TestFixtureSetUp]
        public void WhenFixating()
        {
            _randomNumber = 4;

            _randomNumberGenerator = new Mock<IRandomNumberProvider>();
            _randomNumberGenerator
                .Setup(r => r.Take())
                .Returns(_randomNumber);

            var actor = new RandomActor(_randomNumberGenerator.Object);
            _actorsFixationPosition = actor.Fixate();
        }

        [Test]
        public void ThenANumberIsTakenFromTheRandomNumberProvider()
        {
            _randomNumberGenerator.Verify(r => r.Take());
        }

        [Test]
        public void ThenTheResultIsTheRandomNumber()
        {
            Assert.That(_actorsFixationPosition, Is.EqualTo(_randomNumber));
        }
    }
}