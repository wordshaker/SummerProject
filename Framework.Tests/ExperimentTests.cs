using Framework.TrialRunners;
using Moq;
using NUnit.Framework;

namespace Framework.Tests
{
    [TestFixture]
    public class ExperimentTests
    {
        private Mock<ITrialRunner> _trialRunner;
        private readonly int _numberOfTrials = 3;

        [TestFixtureSetUp]
        public void WhenRunTrialsIsCalled()
        {
            _trialRunner = new Mock<ITrialRunner>();

            var experiment = new Experiment(_trialRunner.Object);
            experiment.RunTrials(_numberOfTrials);
        }

        [Test]
        public void ThenThreeTrialsAreRun()
        {
            _trialRunner.Verify(v => v.Run(), Times.Exactly(_numberOfTrials));
        }
    }
}