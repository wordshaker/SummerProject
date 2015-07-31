using System.Collections.Generic;
using Framework.Actors;
using Framework.Observation;
using Framework.TrialRunners;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.TrialRunners
{
    [TestFixture]
    public class MapTrialRunnerTests
    {
        private Mock<IActor> _intelligentActor;
        private Mock<IMapObservableModel> _observableModel;
        private int _firstFixation;
        private double[] _state;

        [TestFixtureSetUp]
        public void WhenInitialisingALearningTrial()
        {
            _firstFixation = 2;
            _state = new[] {0.25, 0.9, 0.25, 0.1, 0.0, 0.0, 0.0};

            _intelligentActor = new Mock<IActor>();
            _intelligentActor
                .Setup(a => a.Fixate())
                .Returns(_firstFixation);

            _observableModel = new Mock<IMapObservableModel>();
            _observableModel
                .Setup(o => o.GetState(_firstFixation))
                .Returns(_state);

            var trial = new MapTrialRunner(_observableModel.Object, () => _intelligentActor.Object);
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
            _observableModel.Verify(o => o.GetState(_firstFixation));
        }
    }

    [TestFixture]
    public class MapTrialRunnerMoreThenOneFixationTests
    {
        private Mock<IActor> _intelligentActor;
        private Mock<IMapObservableModel> _observableModel;
        private int _firstFixation;
        private int _secondFixation;
        private int _thirdFixation;
        private double[] _state;

        [TestFixtureSetUp]
        public void WhenInitialisingALearningTrial()
        {
            _firstFixation = 4;

            _state = new[] {0.25, 0.9, 0.25, 0.1, 0.0, 0.0, 0.0};

            var fixationQueue = new Queue<int>(new[] {_firstFixation, _secondFixation, _thirdFixation});

            _intelligentActor = new Mock<IActor>();
            _intelligentActor
                .Setup(a => a.Fixate())
                .Returns(() => fixationQueue.Dequeue());

            _observableModel = new Mock<IMapObservableModel>();
            _observableModel
                .Setup(o => o.GetState(_thirdFixation))
                .Returns(_state);

            var trial = new MapTrialRunner(_observableModel.Object, () => _intelligentActor.Object);
            trial.Run();
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheFirstFixation()
        {
            _observableModel.Verify(o => o.GetState(_firstFixation));
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheSEcondFixation()
        {
            _observableModel.Verify(o => o.GetState(_secondFixation));
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheThirdFixation()
        {
            _observableModel.Verify(o => o.GetState(_thirdFixation));
        }

        [Test]
        public void ThenTheSecondFixationIsHighestValueFromTheFirstState()
        {
            var state = _observableModel.Object.GetState(_firstFixation);
            var indexOfMax = 0;
            for (var i = 0; i < state.Length - 1; i++)
            {
                if (state[i] > state[++i])
                {
                    indexOfMax = i;
                }
            }
            Assert.That(_secondFixation, Is.EqualTo(indexOfMax));
        }
    }
}