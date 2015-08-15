using System;
using System.Linq;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;
using Framework.Utilities;

namespace Framework
{
    public class QLearningRecordingTrialRunner : IQLearningTrialRunner
    {
        //where int = fixation location
        private readonly Func<int, IQLearningActor> _actorProvider;
        private readonly IDataRecorder _dataRecorder;
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;

        public QLearningRecordingTrialRunner(IObservableModel observableModel, IRandomNumberProvider randomNumberProvider,
            Func<int, IQLearningActor> actorProvider, IDataRecorder dataRecorder)
        {
            _observableModel = observableModel;
            _randomNumberProvider = randomNumberProvider;
            _actorProvider = actorProvider;
            _dataRecorder = dataRecorder;
        }

        public void Run(IQLearning learning)
        {
            Thread.Sleep(1);
            _observableModel.Generate();

            var fixations = 1;
            var fixationLocation = _randomNumberProvider.Take();
            var beliefState = _observableModel.GetState(fixationLocation);
            var actor = _actorProvider(fixationLocation);
            
            while (beliefState.Any(s => s >= 0.9) == false)//Will return true if the belief state is less than or equal to 0.9
            {
                fixations++;
                beliefState = actor.Fixate();
            }
            _dataRecorder.Insert(fixations);
        }
    }
}