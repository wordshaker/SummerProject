using System;
using System.Linq;
using Accord.Statistics.Distributions.Univariate;
using Framework.Data;
using Framework.Observation;

/**
 * Belief State for creating image of belief states across on trials
 */

namespace Framework.Belief_State
{
    public class BeliefStateForControlsAnalysis : IBeliefStateForControls
    {
        private readonly IBubbleDataRecorder _beliefStateDataRecorder;
        private NormalDistribution _foveaPeripheryOperatingCharacteristic;
        private int _numberOfFixation;

        public BeliefStateForControlsAnalysis(IBubbleDataRecorder beliefStateDataRecorder)
        {
            _beliefStateDataRecorder = beliefStateDataRecorder;
            _numberOfFixation = 0;
        }

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
            ++ _numberOfFixation;
            for (var i = 0; i < activation.Length; i++)
            {
                //discriminability is explained in Butko and Movellan (2008) and is determined using FPOC and eceentricity from fixation.
                var discriminability =
                    new ObservationGenerationModel(_foveaPeripheryOperatingCharacteristic, fixation, i)
                        .GenerateDiscriminabilityValue();
                //This is equation 6 in the Butko & Movellan (2008) paper determining belief state
                State[i] = State[i]*Math.Exp(activation[i]*discriminability);
            }
            //Stores belief states for output onto graph
            _beliefStateDataRecorder.Insert(_numberOfFixation, (double[]) State.Clone());
            return State.Any(s => s >= 0.9);
        }
    }
}