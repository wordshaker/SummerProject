using System;
using Accord.Statistics.Distributions.Univariate;
using Framework.Observation;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Observation
{
    [TestFixture]
    public class ObservationGenerationModelTests
    {
        private Mock<NormalDistribution> _foveaPeripheryOperatingCharacteristic;
        private int _distanceFromFixation;

        [TestFixtureSetUp]
        public void WhenDiscriminabilityValueIsNeeded()
        {
            _foveaPeripheryOperatingCharacteristic = new Mock<NormalDistribution>();

            const int fixation = 1;
            const int location = 4;
            _distanceFromFixation = Math.Abs(fixation - location) - 1;
            var fpoc = new ObservationGenerationModel(_foveaPeripheryOperatingCharacteristic.Object,
                fixation, location);
            fpoc.GenerateDiscriminabilityValue();
        }

        [Test]
        public void ThenProbabilityDensityFunctionCalled()
        {
            _foveaPeripheryOperatingCharacteristic.Verify(f => f.ProbabilityDensityFunction(_distanceFromFixation));
        }
    }
}