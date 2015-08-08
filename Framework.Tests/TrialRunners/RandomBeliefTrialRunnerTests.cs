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
    public class RandomBeliefTrialRunnerTests
    {
        private Mock<IObservableModelForControls> _observableModel;
        private Mock<IActor> _actor;
        private Mock<IDataRecorder> _dataRecorder;
        private int _firstFixation;
        private int _secondFixation;
        private int _thirdFixation;

        [TestFixtureSetUp]
        public void WhenRunningATrial()
        {
            _dataRecorder = new Mock<IDataRecorder>();

            _firstFixation = 3;
            _secondFixation = 1;
            _thirdFixation = 5;
            var fixationQueue = new Queue<int>(new[] {_firstFixation, _secondFixation, _thirdFixation});

            _actor = new Mock<IActor>();
            _actor
                .Setup(a => a.Fixate())
                .Returns(() => fixationQueue.Dequeue());

            _observableModel = new Mock<IObservableModelForControls>();
            _observableModel
                .Setup(o => o.Update(_thirdFixation))
                .Returns(true);

            var trial = new RandomBeliefTrialRunnerWithExclusion(_observableModel.Object, () => _actor.Object,
                _dataRecorder.Object);
            trial.Run();
        }

        [Test]
        public void ThenAnObservableModelIsGenerated()
        {
            _observableModel.Verify(o => o.Generate());
        }

        [Test]
        public void ThenTheActorFixates()
        {
            _actor.Verify(a => a.Fixate(), Times.Exactly(3));
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

        [Test]
        public void ThenTheObservableModelIsUpdatedWithTheThirdFixation()
        {
            _observableModel.Verify(o => o.Update(_thirdFixation));
        }

        [Test]
        public void ThenTheRecorderIsCalledWithTheNumberOfFixations()
        {
            _dataRecorder.Verify(d => d.Insert(3));
        }
    }
}