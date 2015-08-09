using System;
using System.Linq;
using Accord.Statistics.Distributions.Univariate;
using Framework.Observation;

namespace Framework.Belief_State
{
    public class BeliefStateForControls : IBeliefStateForControls
    {
        private NormalDistribution _foveaPeripheryOperatingCharacteristic;
        public double[] State { get; private set; }

        public void Initialise()
        {
            State = new double[70];
            for (var i = 0; i < State.Length; i++)
            {
                State[i] = 1d/7d;
            };
            _foveaPeripheryOperatingCharacteristic = NormalDistribution.Standard;
        }

        public bool Update(double[] activation, int fixation)
        {
            for (var i = 0; i < activation.Length; i++)
            {
                var discriminability =
                    new ObservationGenerationModel(_foveaPeripheryOperatingCharacteristic, fixation, i)
                        .GenerateDiscriminabilityValue();
                State[i] = State[i]*Math.Exp(activation[i]*discriminability);
            }
            return State.Any(s => s >= 0.9);
        }
    }
}