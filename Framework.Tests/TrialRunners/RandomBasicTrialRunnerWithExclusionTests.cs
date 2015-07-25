using System.Collections.Generic;
using Framework.Actors;
using Framework.Data;
using Framework.TrialRunners;
using Framework.VisualArray;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.TrialRunners
{
    [TestFixture]
    public class RandomBasicTrialRunnerWithExclusionTests
    {
        private Mock<IVisualArrayGenerator> _visualArrayGenerator;
        private Mock<IActor> _actor;
        private Mock<IDataRecorder> _dataRecorder;

        [TestFixtureSetUp]
        public void WhenRunningATrial()
        {
            _visualArrayGenerator = new Mock<IVisualArrayGenerator>();
            _visualArrayGenerator
                .Setup(v => v.Generate())
                .Returns(new[] {0, 0, 0, 0, 0, 1, 0});

            var fixationQueue = new Queue<int>(new[] {3, 1, 3, 5});
            _actor = new Mock<IActor>();
            _actor
                .Setup(a => a.Fixate())
                .Returns(() => fixationQueue.Dequeue());

            _dataRecorder = new Mock<IDataRecorder>();

            var trial = new RandomBasicTrialRunnerWithExclusion(_visualArrayGenerator.Object, () => _actor.Object,
                _dataRecorder.Object);
            trial.Run();
        }

        [Test]
        public void ThenAVisualArrayGenerated()
        {
            _visualArrayGenerator.Verify(v => v.Generate());
        }

        [Test]
        public void ThenTheActorFixates()
        {
            _actor.Verify(a => a.Fixate(), Times.Exactly(4));
        }

        [Test]
        public void ThenTheRecorderIsCalledWithUniqueFixations()
        {
            _dataRecorder.Verify(d => d.Insert(3));
        }
    }
}