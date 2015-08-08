using Framework.Belief_State;
using Framework.VisualArray;

namespace Framework.Observation
{
    public class ObservableModelForControls : IObservableModelForControls
    {
        private readonly IActivation _activation;
        private readonly IBeliefStateForControls _beliefState;
        private readonly IVisualArrayGenerator _visualArrayGenerator;
        private int[] _visualArray;

        public ObservableModelForControls(IVisualArrayGenerator visualArrayGenerator, IBeliefStateForControls beliefState,
            IActivation activation)
        {
            _visualArrayGenerator = visualArrayGenerator;
            _beliefState = beliefState;
            _activation = activation;
        }

        public void Generate()
        {
            _visualArray = _visualArrayGenerator.Generate();
            _beliefState.Initialise();
        }

        public bool Update(int fixation)
        {
            var activation = _activation.GenerateActivation(fixation, _visualArray);
            return _beliefState.Update(activation, fixation);
        }
    }
}