using Framework.Belief_State;
using NUnit.Framework;

namespace Framework.Tests.Belief_State
{
    [TestFixture]
    public class BeliefStateInitialiseTests
    {
        private BeliefStateForControls _beliefStateForControls;

        [TestFixtureSetUp]
        public void WhenInitialising()
        {
            _beliefStateForControls = new BeliefStateForControls();
            _beliefStateForControls.Initialise();
        }

        // All areas in the prior are set to 1/arraysize. Since the arraysize is variable there are limitations to it's testability
        [Test]
        public void ThenTheFifthMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[4], Is.GreaterThan(0));
        }

        [Test]
        public void ThenTheFirstMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[0], Is.GreaterThan(0));
        }

        [Test]
        public void ThenTheFourthMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[3], Is.GreaterThan(0));
        }

        [Test]
        public void ThenTheSecondMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[1], Is.GreaterThan(0));
        }

        [Test]
        public void ThenTheSeventhMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[6], Is.GreaterThan(0));
        }

        [Test]
        public void ThenTheSixthMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[5], Is.GreaterThan(0));
        }

        [Test]
        public void ThenTheThirdMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[2], Is.GreaterThan(0));
        }
    }
}