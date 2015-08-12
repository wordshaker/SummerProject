using Framework.Belief_State;
using Framework.Data;
using Framework.VisualArray;

/**
 * Observable model for graphs showing belief state /activation
 */

namespace Framework.Observation
{
    public class ObservableModelForBubble : IObservableModelForControls
    {
        private readonly IActivation _activation;
        private readonly IBubbleDataRecorder _activationDataRecorder;
        private readonly IBeliefStateForControls _beliefState;
        private readonly IVisualArrayGenerator _visualArrayGenerator;
        private int _numberOfFixation;
        private int[] _visualArray;

        public ObservableModelForBubble(IVisualArrayGenerator visualArrayGenerator, IBeliefStateForControls beliefState,
            IActivation activation, IBubbleDataRecorder activationDataRecorder)
        {
            _visualArrayGenerator = visualArrayGenerator;
            _beliefState = beliefState;
            _activation = activation;
            _activationDataRecorder = activationDataRecorder;
            _numberOfFixation = 0;
        }

        public void Generate()
        {
            _visualArray = _visualArrayGenerator.Generate();
            _beliefState.Initialise();
        }

        public bool Update(int fixation)
        {
            ++_numberOfFixation;
            var activation = _activation.GenerateActivation(fixation, _visualArray);
            _activationDataRecorder.Insert(_numberOfFixation, activation);
            return _beliefState.Update(activation, fixation);
        }
    }
}