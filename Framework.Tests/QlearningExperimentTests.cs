using Framework.TrialRunners;
using Moq;
using NUnit.Framework;

namespace Framework.Tests
{
   [TestFixture]
    public class QlearningExperimentTests
    {
        private Mock<IQLearningTrialRunner> _trialRunner;
        private readonly int _numberOfTrials = 3;

        [TestFixtureSetUp]
        public void WhenRunTrialsIsCalled()
        {
            _trialRunner = new Mock<IQLearningTrialRunner>();

            var experiment = new QLearningExperiment(_trialRunner.Object);
            experiment.RunTrials(_numberOfTrials);
        }

        [Test]
        public void ThenThreeTrialsAreRun()
        {
            _trialRunner.Verify(t => t.Run(It.IsAny<IQLearning>()), Times.Exactly(_numberOfTrials));
        }
    }
}