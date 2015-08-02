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

        //Experiments - Basic
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

        public static Experiment CreateMapExperiment()
        {
            var trialRunner = CreateMapTrialRunner();
            return new Experiment(trialRunner);
        }

        //Experiments - Activation Analysis
        public static Experiment CreateRandomBeliefActivationAnalysisExperiment()
        {
            var trialRunner = CreateRandomBeliefActivationChartTrialRunner();
            return new Experiment(trialRunner);
        }

        //Experiments - Belief State Analysis
        public static Experiment CreateRandomBeliefBeliefStateAnalysisExperiment()
        {
            var trialRunner = CreateRandomBeliefBeliefStateAnalysisTrialRunner();
            return new Experiment(trialRunner);
        }

        public static Experiment CreateMapBeliefStateAnalysisExperiment()
        {
            var trialRunner = CreateMapBeliefStateAnalysisTrialRunner();
            return new Experiment(trialRunner);
        }

        //Trial Runners - Basic
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
            var observableModel = CreateMapObservableModel();
            return new MapTrialRunner(observableModel, CreateRandomActor, Repository);
        }

        //Trial Runners - Activation Analysis
        private static ITrialRunner CreateRandomBeliefActivationChartTrialRunner()
        {
            var observableActivationModel = CreateActivationObserverModel();
            return new BubbleAnalysisRunner(observableActivationModel, CreateRandomActor);
        }

        //Trial Runners - Belief State Analysis
        private static ITrialRunner CreateRandomBeliefBeliefStateAnalysisTrialRunner()
        {
            var observableBeliefModel = CreateBeliefStateObserverModel();
            return new BubbleAnalysisRunner(observableBeliefModel, CreateRandomActor);
        }

        private static ITrialRunner CreateMapBeliefStateAnalysisTrialRunner()
        {
            var observableBeliefModel = CreateMapBeliefStateObserverModel();
            return new MapBubbleAnalysisRunner(observableBeliefModel, CreateRandomActor);
        }

        //Utilities - Basic
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

        private static IActivation CreateActivation()
        {
            return new Activation(NormalDistribution.Standard);
        }

        private static IObservableModel CreateObservableModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModel(visualArrayGenerator, new BeliefState(), activation);
        }

        private static IMapObservableModel CreateMapObservableModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForMap(visualArrayGenerator, new BeliefStateForMap(), activation);
        }

        private static IObservableModel CreateActivationObserverModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForBubble(visualArrayGenerator, new BeliefState(), activation,
                ActivationRepository);
        }

        //Observable Models - Belief State Analysis
        private static IObservableModel CreateBeliefStateObserverModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForBubble(visualArrayGenerator, new BeliefStateForAnalysis(BeliefStateRepository),
                activation,
                ActivationRepository);
        }

        private static IMapObservableModel CreateMapBeliefStateObserverModel()
        {
            var visualArrayGenerator = CreateVisualArrayGenerator();
            var activation = CreateActivation();
            return new ObservableModelForMap(visualArrayGenerator,
                new BeliefStateForMapAnalysis(BeliefStateRepository), activation);
        }
    }
}
