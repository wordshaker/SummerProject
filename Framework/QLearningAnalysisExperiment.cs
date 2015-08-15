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
    public class QLearningAnalysisExperiment
    {
        private readonly IDataRecorder _dataRepository;
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;

        public QLearningAnalysisExperiment(IObservableModel observableModel, IRandomNumberProvider randomNumberProvider,
            IDataRecorder dataRepository)
        {
            _observableModel = observableModel;
            _randomNumberProvider = randomNumberProvider;
            _dataRepository = dataRepository;
        }

        public void RunTrials(int trialsToLearn, int trialsToAnalyse)
        {
            var visualArrayLength = 7; //state and action size are the size of the array in this case
            var learning = new QLearningImplementation(0, 0, visualArrayLength, 6);

            var trainingTrialRunner = new QLearningTrialRunner(_observableModel, _randomNumberProvider,
                fixationLocation => new QLearningActor(learning, _observableModel, fixationLocation));
            
            var recordingTrialRunner = new QLearningRecordingTrialRunner(_observableModel, _randomNumberProvider,
                fixationLocation => new QLearningActor(learning, _observableModel, fixationLocation), _dataRepository);

            var learningCount = 0;
            while (learningCount < trialsToLearn)
            {
                trainingTrialRunner.Run(learning);
                learningCount++;
            }

            var analysisCount = 0;
            while (analysisCount < trialsToAnalyse)
            {
                recordingTrialRunner.Run(learning);
                analysisCount++;
            }
        }
    }
}