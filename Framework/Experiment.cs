﻿using System.Threading;
using Framework.TrialRunners;

namespace Framework
{
    public class Experiment
    {
        private readonly ITrialRunner _runTrials;

        public Experiment(ITrialRunner trialRunner)
        {
            _runTrials = trialRunner;
        }

        public void RunTrials(int numberOfTrials)
        {
            Thread.Sleep(1);

            var count = 0;
            while (count < numberOfTrials)
            {
                _runTrials.Run();
                count++;
            }
        }
    }
}