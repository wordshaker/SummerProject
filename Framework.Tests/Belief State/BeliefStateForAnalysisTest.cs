using Framework.Belief_State;
using Framework.Data;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Belief_State
{
    [TestFixture(true, 1, 100, 1, 0.25, 0.25, 0.25, 0.25)]
    [TestFixture(false, 0.1, 0.5, 0.25, 0.25, 0.25, 0.25, 0.25)]
    public class BeliefStateForAnalysisUpdateTests
    {
        private readonly bool _expectedResult;
        private readonly double[] _activation;
        private IBeliefStateForControls _beliefState;
        private bool _result;
        private IBubbleDataRecorder _beliefStateRecorder;

        public BeliefStateForAnalysisUpdateTests(bool expectedResult, double first, double second, double third,
            double fourth,
            double fifth, double sixth, double seventh)
        {
            _expectedResult = expectedResult;

            _activation = new[] {first, second, third, fourth, fifth, sixth, seventh};
        }

        [TestFixtureSetUp]
        public void WhenUpdating()
        {
            _beliefStateRecorder = new AnalysisDataRepository();
            _beliefState = new BeliefStateForControlsAnalysis(_beliefStateRecorder);
            _beliefState.Initialise();
            _result = _beliefState.Update(_activation, 1);
        }

        [Test]
        public void ThenTheResultIsTheExpectedResult()
        {
            Assert.That(_result, Is.EqualTo(_expectedResult));
        }
    }

    public class BeliefStateForAnalysisRecorderTests
    {
        private double[] _activation;
        private IBeliefStateForControls _beliefState;
        private Mock<IBubbleDataRecorder> _beliefStateRecorder;
        private bool _result;

        [TestFixtureSetUp]
        public void WhenUpdating()
        {
            _activation = new[] {0.75, 1.9, 0.75, 0.25, 0.25, 0.25, 0.25};
            _beliefStateRecorder = new Mock<IBubbleDataRecorder>();


            _beliefState = new BeliefStateForControlsAnalysis(_beliefStateRecorder.Object);
            _beliefState.Initialise();
            _result = _beliefState.Update(_activation, 1);
        }
    }
}