using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;

/**
 * Actor for training trials / analysis of QLearning
 */

namespace Framework.Actors
{
    public class QLearningActor : IQLearningActor
    {
        private readonly IObservableModel _observableModel;
        private readonly IQLearning _qLearning;
        private int _fixationLocation;
        

        public QLearningActor(IQLearning qLearning, IObservableModel observableModel, int fixationLocation)
        {
            _observableModel = observableModel;
            _fixationLocation = fixationLocation;
            _qLearning = qLearning;
        }

        public double[] Fixate()
        {
            var previousState = _fixationLocation;
            _fixationLocation = _qLearning.GetAction(_fixationLocation);
            var beliefState = _observableModel.GetState(_fixationLocation);
            var reward = beliefState[_fixationLocation] >= 0.9 ? 10 : -1;// If it is 90% or more likely that area is the target Reward = 10, else reward is -1 (detrimental to actors goals)
            _qLearning.Reward(previousState, _fixationLocation, reward, _fixationLocation);
            return beliefState;
        }
    }
}