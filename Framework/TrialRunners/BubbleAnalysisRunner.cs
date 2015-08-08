using System;
using System.Collections.Generic;
using Framework.Actors;
using Framework.Observation;

namespace Framework.TrialRunners
{
    public class BubbleAnalysisRunner : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModelForControls _observableModel;

        public BubbleAnalysisRunner(IObservableModelForControls observableModel, Func<IActor> actorProvider)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
        }

        public void Run()
        {
            _observableModel.Generate();
            var actor = _actorProvider();
            int fixationLocation;
            var visited = new List<int>();

            do
            {
                fixationLocation = actor.Fixate();
                if (visited.Contains(fixationLocation)) continue;
                visited.Add(fixationLocation);
            } while (_observableModel.Update(fixationLocation) == false);
        }
    }
}