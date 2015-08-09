using System;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.VisualArray;

namespace Framework.TrialRunners
{
    public class RandomBasicTrialRunner : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IDataRecorder _recorder;
        private readonly IVisualArrayGenerator _visualArrayGenerator;

        public RandomBasicTrialRunner(IVisualArrayGenerator visualArrayGenerator, Func<IActor> actorProvider,
            IDataRecorder recorder)
        {
            _visualArrayGenerator = visualArrayGenerator;
            _actorProvider = actorProvider;
            _recorder = recorder;
        }

        public void Run()
        {
            var visualArray = _visualArrayGenerator.Generate();
            var actor = _actorProvider();
            var location = 0;
            var fixations = 0;

            do
            {
                fixations++;
                location = actor.Fixate();
            } while (visualArray[location] == 0);

            _recorder.Insert(fixations);
        }
    }
}