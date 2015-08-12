using System;
using System.Linq;
using Accord.Statistics.Distributions.Univariate;
using Framework.Observation;

/**
 * Belief State for all controls
 */

namespace Framework.Belief_State
{
    public class BeliefStateForControls : IBeliefStateForControls
    {
        private NormalDistribution _foveaPeripheryOperatingCharacteristic;
        public double[] State { get; private set; }

        public void Initialise()
        {
            //Sets prior
            State = new double[7];
            for (var i = 0; i < State.Length; i++)
            {
                State[i] = 1d/7d;
            }
            ;
            _foveaPeripheryOperatingCharacteristic = NormalDistribution.Standard;
        }

        public bool Update(double[] activation, int fixation)
        {
            for (var i = 0; i < activation.Length; i++)
            {
                //discriminability is explained in Butko and Movellan (2008) and is determined using FPOC and eccentricity from fixation.
                var discriminability =
                    new ObservationGenerationModel(_foveaPeripheryOperatingCharacteristic, fixation, i)
                        .GenerateDiscriminabilityValue();
                //This is equation 6 in the Butko & Movellan (2008) paper determining belief state
                State[i] = State[i]*Math.Exp(activation[i]*discriminability);
            }
            return State.Any(s => s >= 0.9);
        }
    }
}