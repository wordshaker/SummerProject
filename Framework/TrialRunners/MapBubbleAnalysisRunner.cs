using System;
using System.Linq;
using System.Threading;
using Accord.Math;
using Framework.Actors;
using Framework.Observation;

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
            _observableModel.Generate();
            var randomActor = _actorProvider();
            var fixationLocation = randomActor.Fixate();
            var state = _observableModel.GetState(fixationLocation);

            while (state.Any(s => s >= 0.9) == false)
            {
                fixationLocation = GetMaxBelief(state);
                state = _observableModel.GetState(fixationLocation);
            }
        }
    }
}