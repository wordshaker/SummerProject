using System.Linq;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;
using Framework.Utilities;

/**
 * Analysis Trial Runner for QLearning (Fixations / Trial)
  */

namespace Framework.TrialRunners
{
    public class QLearningAnalysisTrialRunner : IQLearningTrialRunner
    {
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;
        private readonly IDataRecorder _recorder;

        public QLearningAnalysisTrialRunner(IObservableModel observableModel, IRandomNumberProvider randomNumberProvider,
            IDataRecorder recorder)
        {
            _observableModel = observableModel;
            _recorder = recorder;
            _randomNumberProvider = randomNumberProvider;
        }

        public void Run(IQLearning learning)
        {
            Thread.Sleep(1);
            _observableModel.Generate();

            var fixations = 1;
            var fixationLocation = _randomNumberProvider.Take();
            var beliefState = _observableModel.GetState(fixationLocation);
            var actor = new QLearningActor(learning, _observableModel, fixationLocation);
            while (beliefState.Any(s => s >= 0.9) == false)
            {
                beliefState = actor.Fixate();
                ++fixations;
            }
            _recorder.Insert(fixations);
        }
    }
}