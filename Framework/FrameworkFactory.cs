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
        public static AnalysisDataRepository ActivationRepository;
        public static AnalysisDataRepository BeliefStateRepository;

        static FrameworkFactory()
        {
            Repository = new MemoryDataRepository();
            ActivationRepository = new AnalysisDataRepository();
            BeliefStateRepository = new AnalysisDataRepository();
        }

        //Experiments
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

        public static Experiment CreateRandomBeliefExperiment()
        {
            var trialRunner = CreateRandomBeliefTrialRunner();
            return new Experiment(trialRunner);
        }

        public static Experiment CreateMAPExperiment()
        {
            var trialRunner = CreateMapTrialRunner();
            return new Experiment(trialRunner);
        }

        public static Experiment CreateRandomBeliefBubbleChartExperiment()
        {
            var trialRunner = CreateRandomBeliefBubbleChartTrialRunner();
            return new Experiment(trialRunner);
        }

        public static Experiment CreateRandomBeliefBeliefStateAnalysisExperiment()
        {
            var trialRunner = CreateRandomBeliefBeliefStateAnalysisTrialRunner();
            return new Experiment(trialRunner);
        }

        //Trial Runners
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

        private static ITrialRunner CreateRandomBeliefTrialRunner()
        {
            var observableModel = CreateObservableModel();
            return new RandomBeliefTrialRunner(observableModel, CreateRandomActor, Repository);
        }

        private static ITrialRunner CreateMapTrialRunner()
        {
            var observableModel = CreateIMapObservableModel();
            return new MapTrialRunner(observableModel,CreateRandomActor);
        }

        private static ITrialRunner CreateRandomBeliefBubbleChartTrialRunner()
        {
            var observableBubbleModel = CreateBubbleObserverModel();
            return new BubbleAnalysisRunner(observableBubbleModel, CreateRandomActor);
        }

        private static ITrialRunner CreateRandomBeliefBeliefStateAnalysisTrialRunner()
        {
            var observableBubbleModel = CreateBeliefStateObserverModel();
            return new BubbleAnalysisRunner(observableBubbleModel, CreateRandomActor);
        }

        //Utilities
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

        private static IObservableModel CreateObservableModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModel(visualArrayGenerator, new BeliefState(), activation);
        }

        private static IMapObservableModel CreateIMapObservableModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModel(visualArrayGenerator, new BeliefState(), activation);
        }

        private static IActivation CreateActivation()
        {
            return new Activation(NormalDistribution.Standard);
        }

        private static IObservableBubbleModel CreateBubbleObserverModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForBubble(visualArrayGenerator, new BeliefState(), activation,
                ActivationRepository);
        }

        private static IObservableBubbleModel CreateBeliefStateObserverModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForBubble(visualArrayGenerator, new BeliefStateForAnalysis(BeliefStateRepository),
                activation,
                ActivationRepository);
        }
    }
}