using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;

/**
 * Actor class for test trials of QLearning
 */

namespace Framework.Actors
{
    public class QLearningTestActor : IQLearningActor
    {
        private readonly ICumulativeDataRecorder _dataRecorder;
        private readonly IObservableModel _observableModel;
        private readonly IQLearning _qLearning;
        private int _fixationLocation;

        public QLearningTestActor(IQLearning qLearning, IObservableModel observableModel, int fixationLocation,
            ICumulativeDataRecorder dataRecorder)
        {
            _observableModel = observableModel;
            _fixationLocation = fixationLocation;
            _dataRecorder = dataRecorder;
            _qLearning = qLearning;
        }

        public double[] Fixate()
        {
            var previousState = _fixationLocation;
            _fixationLocation = _qLearning.GetAction(_fixationLocation);
            var beliefState = _observableModel.GetState(_fixationLocation);

            // If it is 90% or more likely that area is the target Reward = 10, else reward is -1 (detrimental to actors goals)
            var reward = beliefState[_fixationLocation] > 0.9 ? 10 : -1;
            //The difference between QLearningTestActor and QLearningActor is rewards are recorded in this & not in the other
            _dataRecorder.Insert(reward);
            _qLearning.Reward(previousState, _fixationLocation, 0, _fixationLocation);
            return beliefState;
        }
    }
}