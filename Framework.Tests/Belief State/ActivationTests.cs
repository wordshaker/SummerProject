using Accord.Statistics.Distributions.Univariate;
using Framework.Belief_State;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Belief_State
{
    [TestFixture]
    internal class ActivationTests
    {
        private Mock<NormalDistribution> _foveaPeripheryOperatingCharacteristic;

        [TestFixtureSetUp]
        public void WhenInitialisingObservationArray()
        {
            _foveaPeripheryOperatingCharacteristic = new Mock<NormalDistribution>();

            var fixation = 0;
            var visualArray = new[] {1, 0, 0, 0, 0, 0, 0};
            var activationValues = new Activation(_foveaPeripheryOperatingCharacteristic.Object);
            activationValues.GenerateActivation(fixation, visualArray);
        }

        [Test]
        public void ThenActivityGaussianIsCalledOnTarget()
        {
            _foveaPeripheryOperatingCharacteristic.Verify(f => f.Generate());
        }

        [Test]
        public void ThenNormalDistributionIsCalledOnNonTargets()
        {
            _foveaPeripheryOperatingCharacteristic.Verify(f => f.Generate(), Times.Exactly(6));
        }
    }
}