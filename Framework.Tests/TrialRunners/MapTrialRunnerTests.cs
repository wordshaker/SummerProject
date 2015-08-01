using System.Collections.Generic;
using Framework.Actors;
using Framework.Data;
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
        private Mock<IDataRecorder> _dataRecorder;

        [TestFixtureSetUp]
        public void WhenInitialisingALearningTrial()
        {
            _dataRecorder = new Mock<IDataRecorder>();

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

            var trial = new MapTrialRunner(_observableModel.Object, () => _intelligentActor.Object, _dataRecorder.Object);
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

        [Test]
        public void ThenTheRecorderIsCalledWithTheNumberOfFixations()
        {
            _dataRecorder.Verify(d => d.Insert(1));
        }
    }

    [TestFixture]
    public class MapTrialRunnerMoreThenOneFixationTests
    {
        private Mock<IActor> _randomActor;
        private Mock<IMapObservableModel> _observableModel;
        private Mock<IDataRecorder> _dataRecorder;

        [TestFixtureSetUp]
        public void WhenInitialisingALearningTrial()
        {
            _dataRecorder = new Mock<IDataRecorder>();

            var firstState = new[] {0.25, 0.35, 0.25, 0.1, 0.4, 0.7, 0.0};
            var secondState = new[] {0.25, 0.60, 0.25, 0.8, 0.0, 0.0, 0.0};
            var thirdState = new[] {0.25, 0.9, 0.25, 0.1, 0.0, 0.0, 0.0};
            var stateQueue = new Queue<double[]>(new[] { firstState, secondState, thirdState });

            _randomActor = new Mock<IActor>();
            _randomActor
                .Setup(a => a.Fixate())
                .Returns(4);

            _observableModel = new Mock<IMapObservableModel>();
            _observableModel
                .Setup(o => o.GetState(It.IsAny<int>()))
                .Returns(stateQueue.Dequeue);

            var trial = new MapTrialRunner(_observableModel.Object, () => _randomActor.Object, _dataRecorder.Object);
            trial.Run();
        }

        [Test]
        public void ThenTheRandomActorIsUsedForTheFirstFixation()
        {
            _randomActor.Verify(a => a.Fixate());
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheFirstFixation()
        {
            _observableModel.Verify(o => o.GetState(4));
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheSecondFixation()
        {
            _observableModel.Verify(o => o.GetState(5));
        }

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheThirdFixation()
        {
            _observableModel.Verify(o => o.GetState(3));
        }

        [Test]
        public void ThenTheRecorderIsCalledWithTheNumberOfFixations()
        {
            _dataRecorder.Verify(d => d.Insert(3));
        }
    }
}