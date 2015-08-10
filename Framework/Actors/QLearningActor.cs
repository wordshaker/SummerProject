using AForge.MachineLearning;
using Framework.Observation;
using Framework.TrialRunners;

namespace Framework.Actors
{
    public class QLearningActor : IQLearningActor
    {
        private readonly IObservableModel _observableModel;
        private int _fixationLocation;
        private readonly IQLearning _qLearning;
        private readonly TabuSearchExploration _tabuPolicy;

        public QLearningActor(IQLearning qLearning, IObservableModel observableModel, int fixationLocation)
        {
            _observableModel = observableModel;
            _fixationLocation = fixationLocation;
            _qLearning = qLearning;
            _tabuPolicy = (TabuSearchExploration)_qLearning.GetPolicy();
        }

        public double[] Fixate()
        {
            var previousState = _fixationLocation;
            _fixationLocation = _qLearning.GetAction(_fixationLocation);
            var beliefState = _observableModel.GetState(_fixationLocation);
            var reward = beliefState[_fixationLocation] > 0.9 ? 10 : -1;
            _qLearning.UpdateState(previousState, _fixationLocation, reward, _fixationLocation);
            _tabuPolicy.SetTabuAction(_fixationLocation, 10);
            return beliefState;
        }
    }
}