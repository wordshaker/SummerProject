using Framework.Belief_State;
using NUnit.Framework;

namespace Framework.Tests.Belief_State
{
    [TestFixture(true, 1, 100, 1, 0.25, 0.25, 0.25, 0.25)]
    [TestFixture(false, 0.89, 0.5, 0.25, 0.25, 0.25, 0.25, 0.25)]
    public class BeliefStateUpdateTests
    {
        private readonly bool _expectedResult;
        private readonly double[] _activation;
        private BeliefState _beliefState;
        private bool _result;

        public BeliefStateUpdateTests(bool expectedResult, double first, double second, double third, double fourth,
            double fifth, double sixth, double seventh)
        {
            _expectedResult = expectedResult;

            _activation = new[] {first, second, third, fourth, fifth, sixth, seventh};
        }

        [TestFixtureSetUp]
        public void WhenUpdating()
        {
            _beliefState = new BeliefState();
            _beliefState.Initialise();

            _result = _beliefState.Update(_activation, 1);
        }

        [Test]
        public void ThenTheResultIsTheExpectedResult()
        {
            Assert.That(_result, Is.EqualTo(_expectedResult));
        }
    }
}