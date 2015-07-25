using System.Collections.Generic;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;
using Moq;
using NUnit.Framework;

namespace Framework.Tests
{
    [TestFixture]
    public class BubbleAnalysisRunnerTests
    {
        private Mock<IObservableBubbleModel> _observableModel;
        private Mock<IActor> _actor;
        private Mock<IBubbleDataRecorder> _dataRecorder;
        private int _firstFixation;
        private int _secondFixation;
        private int _thirdFixation;

        [TestFixtureSetUp]
        public void WhenConstructingBubbleAnalysisGraph()
        {
            _dataRecorder = new Mock<IBubbleDataRecorder>();

            _firstFixation = 3;
            _secondFixation = 1;
            _thirdFixation = 5;
            var fixationQueue = new Queue<int>(new[] {_firstFixation, _secondFixation, _thirdFixation});

            _actor = new Mock<IActor>();
            _actor
                .Setup(a => a.Fixate())
                .Returns(() => fixationQueue.Dequeue());

            _observableModel = new Mock<IObservableBubbleModel>();
            _observableModel
                .Setup(o => o.Update(_thirdFixation))
                .Returns(true);

            var trial = new BubbleAnalysisRunner(_observableModel.Object, () => _actor.Object,
                _dataRecorder.Object);
            trial.Run();
        }

        [Test]
        public void ThenAnObservableModelIsGenerated()
        {
            _observableModel.Verify(o => o.Generate());
        }
    }
}