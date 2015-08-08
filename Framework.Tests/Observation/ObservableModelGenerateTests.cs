using Framework.Belief_State;
using Framework.Observation;
using Framework.VisualArray;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Observation
{
    [TestFixture]
    public class ObservableModelGenerateTests
    {
        private Mock<IVisualArrayGenerator> _visualArrayGenerator;
        private Mock<IBeliefStateForControls> _beliefState;

        [TestFixtureSetUp]
        public void WhenGeneratingAnObservableModel()
        {
            _beliefState = new Mock<IBeliefStateForControls>();
            _visualArrayGenerator = new Mock<IVisualArrayGenerator>();

            var observableModel = new ObservableModelForControls(_visualArrayGenerator.Object, _beliefState.Object, null);
            observableModel.Generate();
        }

        [Test]
        public void ThenABeliefStateIsInitialised()
        {
            _beliefState.Verify(b => b.Initialise());
        }

        [Test]
        public void ThenAVisualArrayIsGenerated()
        {
            _visualArrayGenerator.Verify(v => v.Generate());
        }
    }
}