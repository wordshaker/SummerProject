using System.Linq;
using System.Threading;
using Framework.Actors;
using Framework.Observation;
using Framework.Utilities;

namespace Framework.TrialRunners
{
    internal class QLearningBubbleAnalysisRunner : IQLearningTrialRunner
    {
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;

        public QLearningBubbleAnalysisRunner(IObservableModel observableModel,
            IRandomNumberProvider randomNumberProvider)
        {
            _observableModel = observableModel;
            _randomNumberProvider = randomNumberProvider;
        }

        public void Run(IQLearning learning)
        {
            _observableModel.Generate();

            var fixationLocation = _randomNumberProvider.Take();
            var beliefState = _observableModel.GetState(fixationLocation);
            var actor = new QLearningActor(learning, _observableModel, fixationLocation);

            while (beliefState.Any(s => s >= 0.9) == false)
            {
                beliefState = actor.Fixate();
            }
        }
    }
}