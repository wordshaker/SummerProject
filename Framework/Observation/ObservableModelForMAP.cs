using Framework.Belief_State;
using Framework.VisualArray;

namespace Framework.Observation
{
    public class ObservableModelForMap : IMapObservableModel
    {
        private readonly IActivation _activation;
        private readonly IBeliefStateForMap _beliefState;
        private readonly IVisualArrayGenerator _visualArrayGenerator;
        private int[] _visualArray;

        public ObservableModelForMap(IVisualArrayGenerator visualArrayGenerator, IBeliefStateForMap beliefState,
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

        public double[] GetState(int fixation)
        {
            var activation = _activation.GenerateActivation(fixation, _visualArray);
            return _beliefState.CalculateState(activation, fixation);
        }
    }
}