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
            var count = 0;
            while (count < numberOfTrials)
            {
                _runTrials.Run();
                count++;
            }
        }
    }
}