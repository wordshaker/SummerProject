using Accord.Statistics.Distributions.Univariate;
using Framework.Observation;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Observation
{
    [TestFixture]
    internal class ObservationGenerationModelTests
    {
        private Mock<NormalDistribution> _foveaPeripheryOperatingCharacteristic;
        private int _fixation;
        private int _location;

        [TestFixtureSetUp]
        public void WhenDiscriminabilityValueIsNeeded()
        {
            _foveaPeripheryOperatingCharacteristic = new Mock<NormalDistribution>();

            _fixation = 1;
            _location = 4;
            var FPOC = new ObservationGenerationModel(_foveaPeripheryOperatingCharacteristic.Object,
                _fixation, _location);
            FPOC.GenerateDiscriminabilityValue();
        }

        [Test]
        public void ThenProbabilityDensityFunctionCalled()
        {
            _foveaPeripheryOperatingCharacteristic.Verify(f => f.ProbabilityDensityFunction(_fixation - _location));
        }
    }
}