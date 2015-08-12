using System;
using System.Linq;
using System.Threading;
using Accord.Math;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;

/**
 * MAP trial runner. First fixation random, then following fixations directed by the highest belief value.
 */

namespace Framework.TrialRunners
{
    public class MapTrialRunner : ITrialRunner, IMapTrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModel _observableModel;
        private readonly IDataRecorder _recorder;

        public MapTrialRunner(IObservableModel observableModel, Func<IActor> actorProvider, IDataRecorder recorder)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
            _recorder = recorder;
        }

        public int GetMaxBelief(double[] state)
        {
            //value = max value from the state array
            var value = state.Max(s => s);
            return state.IndexOf(value);
        }

        public void Run()
        {
            Thread.Sleep(1);
            _observableModel.Generate();

            var randomActor = _actorProvider();
            var fixationLocation = randomActor.Fixate();
            var fixations = 1;
            var state = _observableModel.GetState(fixationLocation);

            // will keep looping until a value in the array reaches => 0.9
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