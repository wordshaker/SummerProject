using Framework.Actors;
using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;
using Framework.Utilities;

namespace Framework
{
    public class QLearningRewardAnalysis
    {
        private readonly ICumulativeEpochDataRecorder _cumulativeTrialDataRepository;
        private readonly IObservableModel _observableModel;
        private readonly IRandomNumberProvider _randomNumberProvider;

        public QLearningRewardAnalysis(IObservableModel observableModel, IRandomNumberProvider randomNumberProvider,
            ICumulativeEpochDataRecorder cumulativeTrialDataRecorderRepository)
        {
            _observableModel = observableModel;
            _randomNumberProvider = randomNumberProvider;
            _cumulativeTrialDataRepository = cumulativeTrialDataRecorderRepository;
        }

        public void RunTrials(int numberOfTrials)
        {
            var visualArrayLength = 7; //state and action size are the size of the array in this case
            var learning = new QLearningImplementation(0.5, 0.5, visualArrayLength, 6);

            var trainingTrialRunner = new QLearningTrialRunner(_observableModel, _randomNumberProvider,
                fixationLocation =>
                    new QLearningEpochActor(learning, _observableModel, fixationLocation, _cumulativeTrialDataRepository));

            var count = 0;
            while (count < numberOfTrials)
            {
                trainingTrialRunner.Run(learning);
                count++;
                if (count%150 == 0 && count != 0)
                {
                    _cumulativeTrialDataRepository.IncrementTrial(count);
                }
            }
        }
    }
}