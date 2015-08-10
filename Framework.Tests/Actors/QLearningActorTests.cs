using AForge.MachineLearning;
using Framework.Actors;
using Framework.Observation;
using Framework.TrialRunners;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Actors
{
    [TestFixture]
    public class QLearningActorTests
    {
        private static Mock<IQLearning> _learning;
        private static Mock<IObservableModel> _observableModel;
        private int _fixationLocation;
        private int _previousState;
        private double _reward;

        [TestFixtureSetUp]
        public void WhenQLearningActorInitialised()
        {
            _previousState = 5;
            _fixationLocation = 3;
            _reward = -1;

            _observableModel = new Mock<IObservableModel>();
            _observableModel
                .Setup(o => o.GetState(It.IsAny<int>()))
                .Returns(new[]{ 0,0,0,_reward,0,0,0});

            var tabuPolicy = new Mock<TestTabuSearchExploration>();
            _learning = new Mock<IQLearning>();
            _learning
                .Setup(l => l.GetAction(It.IsAny<int>()))
                .Returns(_fixationLocation);
            _learning
                .Setup(l => l.GetPolicy())
                .Returns(tabuPolicy.Object);

            var actor = new QLearningActor(_learning.Object, _observableModel.Object, _previousState);
            actor.Fixate();
        }

        [Test]
        public void ThenGetActionIsCAlledOnQLearning()
        {
            _learning.Verify(l => l.GetAction(_previousState));
        }
        
        [Test]
        public void ThenGetStateIsCalledOnObservableModel()
        {
            _observableModel.Verify(o => o.GetState(_fixationLocation));
        }

        [Test] public void ThenUpdateStateIsCalLedOnQLearning()
        {
            _learning.Verify(l=> l.UpdateState(_previousState, _fixationLocation, _reward, _fixationLocation));
        }
    }

    public class TestTabuSearchExploration : TabuSearchExploration
    {
        public TestTabuSearchExploration()
            : base(60, new Mock<IExplorationPolicy>().Object)
        {
        }
    }
}