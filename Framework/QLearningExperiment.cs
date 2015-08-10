using System.Threading;
using AForge.MachineLearning;
using Framework.TrialRunners;
/**
 * QLearningWrapper wraps the QLearning Class Available in the AForge.MachineLearning Library
 * Reference : http://www.aforgenet.com/framework/docs/html/e5835f25-ea5c-4d62-c6a3-592da5d9fa59.htm
 */
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
            var visualArrayLength = 60; //state and action size are the size of the array in this case
            var temperature = 0.1;

            var boltzmannPolicy = new BoltzmannExploration(temperature);
            var explorationPolicy = new TabuSearchExploration(visualArrayLength, boltzmannPolicy);
            var learning = new QLearningWrapper(visualArrayLength, visualArrayLength, explorationPolicy);

            var count = 0;
            while (count < numberOfTrials)
            {
                explorationPolicy.ResetTabuList();
                boltzmannPolicy.Temperature = temperature + ((double)count / numberOfTrials);
                _runTrials.Run(learning);
                count++;
            }
        }
    }
}