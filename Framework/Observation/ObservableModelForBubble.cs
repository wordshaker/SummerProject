using Framework.Belief_State;
using Framework.Data;
using Framework.VisualArray;

namespace Framework.Observation
{
    public class ObservableModelForBubble : IObservableModel, IMapObservableModel
    {
        private readonly IActivation _activation;
        private readonly IBubbleDataRecorder _activationDataRecorder;
        private readonly IBeliefState _beliefState;
        private readonly IVisualArrayGenerator _visualArrayGenerator;
        private int _numberOfFixation;
        private int[] _visualArray;

        public ObservableModelForBubble(IVisualArrayGenerator visualArrayGenerator, IBeliefState beliefState,
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

        public double[] GetState(int fixation)
        {
            var activation = _activation.GenerateActivation(fixation, _visualArray);
            return _beliefState.CalculateState(activation, fixation);
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