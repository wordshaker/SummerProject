using System;
using System.Linq;
using Accord.Math;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;

namespace Framework.TrialRunners
{
    public class MapTrialRunner : ITrialRunner, IMapTrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IMapObservableModel _observableModel;
        private readonly IDataRecorder _recorder;

        public MapTrialRunner(IMapObservableModel observableModel, Func<IActor> actorProvider, IDataRecorder recorder)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
            _recorder = recorder;
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
            var fixations = 1;
            var state = _observableModel.GetState(fixationLocation);

            while (state.Any(s => s >= 0.9) == false)
            {
                fixationLocation = GetMaxBelief(state);
                state = _observableModel.GetState(fixationLocation);
                ++fixations;
            }
            _recorder.Insert(fixations);
        }
    }
}