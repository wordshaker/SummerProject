using System;
using System.Collections.Generic;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;

/**
 * Using FPOC, actor completes when =>90% certain of the targets location.
 * Without replacement - each area of VA can only be fixated on onceS#.
 * Fixations made randomly, but actor has more information about environment
 */

namespace Framework.TrialRunners
{
    public class RandomBeliefTrialRunnerWithExclusion : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModelForControls _observableModel;
        private readonly IDataRecorder _recorder;

        public RandomBeliefTrialRunnerWithExclusion(IObservableModelForControls observableModel,
            Func<IActor> actorProvider,
            IDataRecorder recorder)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
            _recorder = recorder;
        }

        public void Run()
        {
            Thread.Sleep(1);
            _observableModel.Generate();
            var actor = _actorProvider();
            var fixations = 0;
            int fixationLocation;
            var visited = new List<int>();

            do
            {
                fixationLocation = actor.Fixate();
                if (visited.Contains(fixationLocation)) continue;
                fixations++;
                visited.Add(fixationLocation);
            } while (_observableModel.Update(fixationLocation) == false);

            _recorder.Insert(fixations);
        }
    }
}