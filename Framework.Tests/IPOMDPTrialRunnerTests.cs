using System;
using System.Collections.Generic;
using Framework.Actors;
using Framework.Observation;
using Framework.TrialRunners;
using Framework.VisualArray;
using Moq;
using NUnit.Framework;

namespace Framework.Tests
{
    [TestFixture]
    public class IPOMDPTrialRunnerTests
    {
        private Mock<IVisualArrayGenerator> _visualArrayGenerator;
        private Mock<IActor> _intelligentActor;
        private Mock<IObservableModel> _observableModel;
        private int _firstFixation;
        private int _secondFixation;
        private int _thirdFixation;

        [TestFixtureSetUp]
        public void WhenInitialisingALearningTrial()
        {
            _firstFixation = 3;
            _secondFixation = 1;
            _thirdFixation = 5;
            var fixationQueue = new Queue<int>(new[] {_firstFixation, _secondFixation, _thirdFixation});

            _intelligentActor = new Mock<IActor>();
            _intelligentActor
                .Setup(a => a.Fixate())
                .Returns(() => fixationQueue.Dequeue());

            _observableModel = new Mock<IObservableModel>();
            _observableModel
                .Setup(o => o.Update(_thirdFixation))
                .Returns(true);

            var trial = new IPOMDPTrialRunner(_observableModel.Object, () => _intelligentActor.Object);
            trial.Run();
        }

        [Test]
        public void ThenAnIntelligentActorFixates()
        {
            _intelligentActor.Verify(i => i.Fixate());
        }

        [Test]
        public void ThenAnObservableModelIsGenerated()
        {
            _observableModel.Verify(o => o.Generate());
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheFirstFixation()
        {
            _observableModel.Verify(o => o.Update(_firstFixation));
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheSecondFixation()
        {
            _observableModel.Verify(o => o.Update(_secondFixation));
        } 
    }

    public interface IIntelligentActor
    {
        void Update();
    }

    public class IPOMDPTrialRunner : ITrialRunner
    {
        private readonly Func<IActor> _actorProvider;
        private readonly IObservableModel _observableModel;

        public IPOMDPTrialRunner(IObservableModel observableModel, Func<IActor> actorProvider)
        {
            _observableModel = observableModel;
            _actorProvider = actorProvider;
        }

        public void Run()
        {
            _observableModel.Generate();
            var intelligentActor = _actorProvider();
            int fixationLocation;

            do
            {
                fixationLocation = intelligentActor.Fixate();
            } while (_observableModel.Update(fixationLocation) == false);
        }
    }
}