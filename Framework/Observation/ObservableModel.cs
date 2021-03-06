using Framework.Belief_State;
using Framework.VisualArray;

/**
 * Observable models for trials that arent control trials
 */

namespace Framework.Observation
{
    public class ObservableModel : IObservableModel
    {
        private readonly IActivation _activation;
        private readonly IBeliefState _beliefState;
        private readonly IVisualArrayGenerator _visualArrayGenerator;
        private int[] _visualArray;

        public ObservableModel(IVisualArrayGenerator visualArrayGenerator, IBeliefState beliefState,
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