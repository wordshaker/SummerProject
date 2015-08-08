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

        [Test]
        public void ThenTheFifthMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[4], Is.EqualTo(0.14285714285714285d));
        }

        [Test]
        public void ThenTheFirstMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[0], Is.EqualTo(0.14285714285714285d));
        }

        [Test]
        public void ThenTheFourthMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[3], Is.EqualTo(0.14285714285714285d));
        }

        [Test]
        public void ThenTheSecondMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[1], Is.EqualTo(0.14285714285714285d));
        }

        [Test]
        public void ThenTheSeventhMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[6], Is.EqualTo(0.14285714285714285d));
        }

        [Test]
        public void ThenTheSixthMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[5], Is.EqualTo(0.14285714285714285d));
        }

        [Test]
        public void ThenTheStateArrayHasSevenMembers()
        {
            Assert.That(_beliefStateForControls.State.Length, Is.EqualTo(7));
        }

        [Test]
        public void ThenTheThirdMemberOfTheStateArrayIsOne()
        {
            Assert.That(_beliefStateForControls.State[2], Is.EqualTo(0.14285714285714285d));
        }
    }
}