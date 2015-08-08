using System.Threading;
using Framework.Observation;
using Framework.TrialRunners;

namespace Framework.Actors
{
    public class QLearningActor : IQLearningActor
    {
        private readonly IObservableModel _observableModel;
        private int _fixationLocation;
        private readonly IQLearning _qLearning;

        public QLearningActor(IQLearning qLearning, IObservableModel observableModel, int fixationLocation)
        {
            _observableModel = observableModel;
            _fixationLocation = fixationLocation;
            _qLearning = qLearning;
        }

        public double[] Fixate()
        {
            Thread.Sleep(1);
            var previousState = _fixationLocation;
            _fixationLocation = _qLearning.GetAction(_fixationLocation);
            var beliefState = _observableModel.GetState(_fixationLocation);
            var reward = beliefState[_fixationLocation];
            _qLearning.UpdateState(previousState, _fixationLocation, reward, _fixationLocation);
            return beliefState;
        }
    }
}