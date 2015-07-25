using Framework.Belief_State;
using Framework.Data;
using Framework.VisualArray;

namespace Framework.Observation
{
    public class ObservableModelForBubble : IObservableBubbleModel
    {
        private readonly IActivation _activation;
        private readonly IBeliefState _beliefState;
        private readonly IVisualArrayGenerator _visualArrayGenerator;
        private int[] _visualArray;
        private readonly IBubbleDataRecorder _activationDataRecorder;
        private int _numberOfFixation;

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

        public bool Update(int fixation)
        {
            ++_numberOfFixation;
            var activation = _activation.GenerateActivation(fixation, _visualArray);
            _activationDataRecorder.Insert(_numberOfFixation, activation);
            return _beliefState.Update(activation, fixation);
        }
    }

    public interface IObservableBubbleModel
    {
        void Generate();
        bool Update(int fixation);
    }
}