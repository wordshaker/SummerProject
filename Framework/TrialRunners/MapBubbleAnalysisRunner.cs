using System;
using System.Linq;
using System.Threading;
using Accord.Math;
using Framework.Actors;
using Framework.Observation;

/**
 * Trial runner for MAPS bubble graphs
 */

namespace Framework.TrialRunners
{
    public class MapBubbleAnalysisRunner : ITrialRunner, IMapTrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModel _observableModel;

        public MapBubbleAnalysisRunner(IObservableModel observableModel, Func<IActor> actorProvider)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
        }

        public int GetMaxBelief(double[] state)
        {
            var value = state.Max(s => s);
            return state.IndexOf(value);
        }

        public void Run()
        {
            Thread.Sleep(1);
            _observableModel.Generate();
            var randomActor = _actorProvider();
            var fixationLocation = randomActor.Fixate();
            var state = _observableModel.GetState(fixationLocation);

            // While the goal hasnt been reached (any value in the state array not >= 0.9)
            while (state.Any(s => s >= 0.9) == false)
            {
                fixationLocation = GetMaxBelief(state);
                state = _observableModel.GetState(fixationLocation);
            }
        }
    }
}