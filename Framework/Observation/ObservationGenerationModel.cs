using System;
using Accord.Statistics.Distributions.Univariate;

namespace Framework.Observation
{
    public class ObservationGenerationModel
    {
        private readonly int _fixation;
        private readonly NormalDistribution _foveaPeripheryOperatingCharacteristic;
        private readonly int _location;

        public ObservationGenerationModel(NormalDistribution foveaPeripheryOperatingCharacteristic,
            int fixation,
            int location)
        {
            _foveaPeripheryOperatingCharacteristic = foveaPeripheryOperatingCharacteristic;
            _fixation = fixation;
            _location = location;
        }

        public double GenerateDiscriminabilityValue()
        {
            var distanceFromFixation = Math.Abs(_fixation - _location);
            //
            if (distanceFromFixation > 0)
            {
                distanceFromFixation--;
            }
            return _foveaPeripheryOperatingCharacteristic.ProbabilityDensityFunction(distanceFromFixation)*8;
        }
    }
}