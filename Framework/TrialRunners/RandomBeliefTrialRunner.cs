using System;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;

/**
 * Using FPOC, actor completes when =>90% certain of the targets location.
 * Fixations made randomly, but actor has more information about environment
 */

namespace Framework.TrialRunners
{
    public class RandomBeliefTrialRunner : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModelForControls _observableModelForControls;
        private readonly IDataRecorder _recorder;

        public RandomBeliefTrialRunner(IObservableModelForControls observableModelForControls,
            Func<IActor> actorProvider,
            IDataRecorder recorder)
        {
            _observableModelForControls = observableModelForControls;
            _actorProvider = actorProvider;
            _recorder = recorder;
        }

        public void Run()
        {
            Thread.Sleep(1);
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