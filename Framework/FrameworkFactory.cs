using Accord.Statistics.Distributions.Univariate;
using Framework.Actors;
using Framework.Belief_State;
using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;
using Framework.Utilities;
using Framework.VisualArray;

namespace Framework
{
    public static class FrameworkFactory
    {
        public static MemoryDataRepository Repository;
        public static ActivationDataRepository ActivationRepository;

        static FrameworkFactory()
        {
            Repository = new MemoryDataRepository();
            ActivationRepository = new ActivationDataRepository();
        }

        public static Experiment CreateRandomExperiment()
        {
            var trialRunner = CreateRandomTrialRunner();
            return new Experiment(trialRunner);
        }

        public static Experiment CreateRandomExclusionExperiment()
        {
            var trialRunner = CreateRandomExclusionTrialRunner();
            return new Experiment(trialRunner);
        }

        private static ITrialRunner CreateRandomTrialRunner()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            return new RandomBasicTrialRunner(visualArrayGenerator, CreateRandomActor, Repository);
        }

        private static ITrialRunner CreateRandomExclusionTrialRunner()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            return new RandomBasicTrialRunnerWithExclusion(visualArrayGenerator, CreateRandomActor, Repository);
        }

        private static IActor CreateRandomActor()
        {
            var randomNumberProvider = new ZeroToSixRandomNumberProvider();
            return new RandomActor(randomNumberProvider);
        }

        private static IVisualArrayGenerator CreateVisualArrayGenerator()
        {
            var randomNumberProvider = new ZeroToSixRandomNumberProvider();
            return new VisualArrayGenerator(randomNumberProvider);
        }

        public static Experiment CreateRandomBeliefExperiment()
        {
            var trialRunner = CreateRandomBeliefTrialRunner();
            return new Experiment(trialRunner);
        }

        private static ITrialRunner CreateRandomBeliefTrialRunner()
        {
            var observableModel = CreateObservableModel();
            return new RandomBeliefTrialRunner(observableModel, CreateRandomActor, Repository);
        }

        private static IObservableModel CreateObservableModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModel(visualArrayGenerator, new BeliefState(), activation);
        }

        private static IActivation CreateActivation()
        {
            return new Activation(NormalDistribution.Standard);
        }

        public static Experiment CreateRandomBeliefBubbleChartExperiment()
        {
            var trialRunner = CreateRandomBeliefBubbleChartTrialRunner();
            return new Experiment(trialRunner);
        }

        private static ITrialRunner CreateRandomBeliefBubbleChartTrialRunner()
        {
            var obsevableBubbleModel = CreateBubbleOberserModel();
            return new BubbleAnalysisRunner(obsevableBubbleModel, CreateRandomActor);
        }

        private static IObservableBubbleModel CreateBubbleOberserModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForBubble(visualArrayGenerator, new BeliefState(), activation, ActivationRepository);
        }
    }
}