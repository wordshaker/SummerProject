using Framework.Actors;
using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;
using Framework.Utilities;

/**
 * QLearningWrapper wraps the QLearning Class Available in the AForge.MachineLearning Library
 * Reference : http://www.aforgenet.com/framework/docs/html/e5835f25-ea5c-4d62-c6a3-592da5d9fa59.htm
 */

namespace Framework
{
    public class QLearningExperiment
    {
        private readonly ICumulativeDataRecorder _cumulativeDataRepository;
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;

        public QLearningExperiment(IObservableModel observableModel, IRandomNumberProvider randomNumberProvider,
            ICumulativeDataRecorder cumulativeDataRepository)
        {
            _observableModel = observableModel;
            _randomNumberProvider = randomNumberProvider;
            _cumulativeDataRepository = cumulativeDataRepository;
        }

        public void RunTrials(int numberOfTrials)
        {
            var visualArrayLength = 7; //state and action size are the size of the array in this case
            var learning = new QLearningImplementation(0.75, 0.8, visualArrayLength, 6);

            var trainingTrialRunner = new QLearningTrialRunner(_observableModel, _randomNumberProvider,
                fixationLocation => new QLearningActor(learning, _observableModel, fixationLocation));

            var testingTrialRunner = new QLearningTrialRunner(_observableModel, _randomNumberProvider,
                fixationLocation =>
                    new QLearningTestActor(learning, _observableModel, fixationLocation, _cumulativeDataRepository));

            var count = 0;
            while (count < numberOfTrials)
            {
                trainingTrialRunner.Run(learning);
                count++;
                
                if (count%150 == 0 && count != 0)
                {
                    testingTrialRunner.Run(learning);
                    _cumulativeDataRepository.IncrementTrial(count);
                }
            }
        }
    }
}