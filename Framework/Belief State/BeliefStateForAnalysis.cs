using System;
using Accord.Statistics.Distributions.Univariate;
using Framework.Data;
using Framework.Observation;

namespace Framework.Belief_State
{
    public class BeliefStateForAnalysis : IBeliefState
    {
        private readonly IBubbleDataRecorder _beliefStateDataRecorder;
        private NormalDistribution _foveaPeripheryOperatingCharacteristic;
        private int _numberOfFixation;

        public BeliefStateForAnalysis(IBubbleDataRecorder beliefStateDataRecorder)
        {
            _beliefStateDataRecorder = beliefStateDataRecorder;
            _numberOfFixation = 0;
        }

        public double[] State { get; private set; }

        public void Initialise()
        {
            State = new double[7];
            for (var i = 0; i < State.Length; i++)
            {
                State[i] = 1d/7d;
            }
            ;
            _foveaPeripheryOperatingCharacteristic = NormalDistribution.Standard;
        }

        public double[] CalculateState(double[] activation, int fixation)
        {
            ++_numberOfFixation;
            for (var i = 0; i < activation.Length; i++)
            {
                var discriminability =
                    new ObservationGenerationModel(_foveaPeripheryOperatingCharacteristic, fixation, i)
                        .GenerateDiscriminabilityValue();
                State[i] = State[i]*Math.Exp(activation[i]*discriminability);
            }
            _beliefStateDataRecorder.Insert(_numberOfFixation, (double[]) State.Clone());
            return State;
        }
    }
}