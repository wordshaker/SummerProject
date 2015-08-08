using Framework.Actors;
using Framework.Observation;
using Framework.TrialRunners;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Actors
{
    [TestFixture]
    internal class QLearningActorTests
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
            _reward = 0.75;

            _observableModel = new Mock<IObservableModel>();
            _observableModel
                .Setup(o => o.GetState(It.IsAny<int>()))
                .Returns(new[]{ 0,0,0,_reward,0,0,0});

            _learning = new Mock<IQLearning>();
            _learning
                .Setup(l => l.GetAction(It.IsAny<int>()))
                .Returns(_fixationLocation);

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
}