using System;
using System.Linq;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;

namespace Framework.TrialRunners
{
    public class QLearningTrialRunner
    {
        private readonly Func<IIntelligentActor> _actorProvider;
        private readonly IMapObservableModel _observableModel;
        private readonly IDataRecorder _recorder;

        public QLearningTrialRunner(IMapObservableModel observableModel, Func<IIntelligentActor> actorProvider,
            IDataRecorder recorder)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
            _recorder = recorder;
        }

        public void Run()
        {
            _observableModel.Generate();

            var intelligentActor = _actorProvider();
            var fixationLocation = intelligentActor.Fixate();
            var fixations = 1;
            var state = _observableModel.GetState(fixationLocation);

            while (state.Any(s => s >= 0.9) == false)
            {
                fixationLocation = intelligentActor.IntelligentFixation(state);
                state = _observableModel.GetState(fixationLocation);
                ++fixations;
            }
            _recorder.Insert(fixations);
        }
    }
}