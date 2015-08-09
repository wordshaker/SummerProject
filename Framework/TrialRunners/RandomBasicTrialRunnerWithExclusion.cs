using System;
using System.Collections.Generic;
using System.Threading;
using Framework.Actors;
using Framework.Data;
using Framework.VisualArray;

namespace Framework.TrialRunners
{
    public class RandomBasicTrialRunnerWithExclusion : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IDataRecorder _recorder;
        private readonly IVisualArrayGenerator _visualArrayGenerator;

        public RandomBasicTrialRunnerWithExclusion(IVisualArrayGenerator visualArrayGenerator,
            Func<IActor> actorProvider,
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
            var visited = new List<int>();
            do
            {
                var next = actor.Fixate();
                if (visited.Contains(next)) continue;

                fixations++;
                visited.Add(next);
                location = next;
            } while (visualArray[location] == 0);

            _recorder.Insert(fixations);
        }
    }
}