using System;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;

namespace Framework.TrialRunners
{
    public class RandomBeliefTrialRunner : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModelForControls _observableModelForControls;
        private readonly IDataRecorder _recorder;

        public RandomBeliefTrialRunner(IObservableModelForControls observableModelForControls, Func<IActor> actorProvider,
            IDataRecorder recorder)
        {
            _observableModelForControls = observableModelForControls;
            _actorProvider = actorProvider;
            _recorder = recorder;
        }

        public void Run()
        {
            _observableModelForControls.Generate();
            var actor = _actorProvider();
            var fixations = 0;
            int fixationLocation;

            do
            {
                fixationLocation = actor.Fixate();
                fixations++;
            } while (_observableModelForControls.Update(fixationLocation) == false);

            _recorder.Insert(fixations);
        }
    }
}