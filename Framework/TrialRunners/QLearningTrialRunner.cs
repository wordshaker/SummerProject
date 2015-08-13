using System;
using System.Linq;
using System.Threading;
using Framework.Actors;
using Framework.Observation;
using Framework.Utilities;

/**
 * QLearning Trial Runner
 */

namespace Framework.TrialRunners
{
    public class QLearningTrialRunner : IQLearningTrialRunner
    {
        //where int = fixation location
        private readonly Func<int, IQLearningActor> _actorProvider;
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;
        private double _totalReward;

        public QLearningTrialRunner(IObservableModel observableModel, IRandomNumberProvider randomNumberProvider,
            Func<int, IQLearningActor> actorProvider)
        {
            _observableModel = observableModel;
            _randomNumberProvider = randomNumberProvider;
            _actorProvider = actorProvider;
        }

        public void Run(IQLearning learning)
        {
            Thread.Sleep(1);
            _observableModel.Generate();

            var fixationLocation = _randomNumberProvider.Take();
            var beliefState = _observableModel.GetState(fixationLocation);
            var actor = _actorProvider(fixationLocation);
            
            while (beliefState.Any(s => s >= 0.9) == false)//Will return true if the belief state is less than or equal to 0.9
            {
                beliefState = actor.Fixate();
            }
        }
    }
}