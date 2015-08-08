using AForge.MachineLearning;
using Framework.TrialRunners;

namespace Framework
{
    public class QLearningExperiment
    {
        private readonly IQLearningTrialRunner _runTrials;

        public QLearningExperiment(IQLearningTrialRunner trialRunner)
        {
            _runTrials = trialRunner;
        }

        public void RunTrials(int numberOfTrials)
        {
            var epsilon = 0.5;
            var learningRate = 0.5;
            var explorationPolicy = new EpsilonGreedyExploration(epsilon);
            var learning = new QLearningWrapper(7, 7, explorationPolicy);
            var count = 0;
            while (count < numberOfTrials)
            {
                explorationPolicy.Epsilon = epsilon - ((double) count/numberOfTrials)*epsilon;
                learning.LearningRate = learningRate - ((double) count/numberOfTrials)*learningRate;

                _runTrials.Run(learning);
                count++;
            }
        }
    }
}