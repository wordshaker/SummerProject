using Framework.Belief_State;
using Framework.Observation;
using Framework.VisualArray;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Observation
{
    [TestFixture(false)]
    [TestFixture(true)]
    public class ObservableModelUpdateTests
    {
        private bool _result;
        private readonly bool _expectedResult;
        private Mock<IBeliefStateForControls> _beliefState;
        private Mock<IActivation> _activation;
        private int _fixation;
        private int[] _visualArray;
        private double[] _activationResults;

        public ObservableModelUpdateTests(bool expectedResult)
        {
            _expectedResult = expectedResult;
        }

        [TestFixtureSetUp]
        public void WhenGeneratingAnObservableModel()
        {
            _fixation = 3;
            _visualArray = new[] {1};
            _activationResults = new[] {2.2};

            _beliefState = new Mock<IBeliefStateForControls>();
            _beliefState
                .Setup(b => b.Update(It.IsAny<double[]>(), It.IsAny<int>()))
                .Returns(_expectedResult);

            var visualArrayGenerator = new Mock<IVisualArrayGenerator>();
            visualArrayGenerator
                .Setup(v => v.Generate())
                .Returns(_visualArray);

            _activation = new Mock<IActivation>();
            _activation
                .Setup(o => o.GenerateActivation(It.IsAny<int>(), It.IsAny<int[]>()))
                .Returns(_activationResults);

            var observableModel = new ObservableModelForControls(visualArrayGenerator.Object, _beliefState.Object,
                _activation.Object);

            observableModel.Generate();
            _result = observableModel.Update(_fixation);
        }

        [Test]
        public void ThenTheBeliefStateIsUpdatedWithTheActivation()
        {
            _beliefState.Verify(b => b.Update(_activationResults, _fixation));
        }

        [Test]
        public void ThenTheObservationGenerationModelGeneratesTheActivation()
        {
            _activation.Verify(o => o.GenerateActivation(_fixation, _visualArray));
        }

        [Test]
        public void ThenTheResultIsTheExpectedResult()
        {
            Assert.That(_result, Is.EqualTo(_expectedResult));
        }
    }
}