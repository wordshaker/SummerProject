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
    public class QLearningTrialRunnerTests
    {
        private Mock<IMapObservableModel> _observableModel;
        private Mock<IIntelligentActor> _intelligentActor;
        private Mock<IDataRecorder> _dataRecorder;
        private int _fixationOne;
        private double[] _firstState;

        [TestFixtureSetUp]
        public void WhenInitialisingAQLearningTrial()
        {
            _fixationOne = 3;
            _firstState = new[] {0.25, 0.35, 0.25, 0.1, 0.4, 0.7, 0.0};
            var secondState = new[] {0.25, 0.60, 0.25, 0.8, 0.0, 0.0, 0.0};
            var thirdState = new[] {0.25, 0.9, 0.25, 0.1, 0.0, 0.0, 0.0};
            var stateQueue = new Queue<double[]>(new[] {_firstState, secondState, thirdState});


            _intelligentActor = new Mock<IIntelligentActor>();
            _intelligentActor
                .Setup(i => i.Fixate())
                .Returns(_fixationOne);

            _intelligentActor
                .Setup(i => i.IntelligentFixation(_firstState))
                .Returns(3);

            _observableModel = new Mock<IMapObservableModel>();
            _observableModel
                .Setup(o => o.GetState(It.IsAny<int>()))
                .Returns(stateQueue.Dequeue);

            _dataRecorder = new Mock<IDataRecorder>();

            var trial = new QLearningTrialRunner(_observableModel.Object, () => _intelligentActor.Object,
                _dataRecorder.Object);
            trial.Run();
        }

        [Test]
        public void ThenIntelligentActorFixatesUsingNewInformation()
        {
            _intelligentActor.Verify(i => i.IntelligentFixation(_firstState));
        }

        [Test]
        public void ThenIntelligentActorFixatesUsingPrior()
        {
            _intelligentActor.Verify(i => i.Fixate());
        }

        [Test]
        public void ThenObservableModelGenerated()
        {
            _observableModel.Verify(o => o.Generate());
        }

        [Test]
        public void ThenStateGeneratedUsingFirstFixation()
        {
            _observableModel.Verify(o => o.GetState(_fixationOne));
        }

        [Test]
        public void ThenTheRecorderIsCalledWithTheNumberOfFixations()
        {
            _dataRecorder.Verify(d => d.Insert(3));
        }
    }
}