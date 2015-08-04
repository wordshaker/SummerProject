using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AForge.MachineLearning;
using AForge.Math.Random;
using Framework.Actors;
using Framework.Utilities;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.Actors
{
    [TestFixture]
    class IntelligentActorFirstFixationTests 
    {
        private Mock<IRandomNumberProvider> _randomNumberGenerator;
        private int _actorsFirstFixation;
        private int _randomNumber;

        [TestFixtureSetUp]
        public void WhenMakingFirstFixation()
        {
            _randomNumber = 4;
            _randomNumberGenerator = new Mock<IRandomNumberProvider>();
            _randomNumberGenerator
                .Setup(r => r.Take())
                .Returns(_randomNumber);

            var actor = new IntelligentActor(_randomNumberGenerator.Object);
            _actorsFirstFixation = actor.Fixate();
        }

        [Test]
        public void ThenANumberIsTakenFromTheRandomNumberProvider()
        {
            _randomNumberGenerator.Verify(r => r.Take());
        }

        [Test]
        public void ThenTheResultIsTheRandomNumber()
        {
            Assert.That(_actorsFirstFixation, Is.EqualTo(_randomNumber));
        }
    }

    [TestFixture]
    class IntelligentActorIntelligentFixationTests
    {
        private double[] _state;
        private Mock<IRandomNumberProvider> _randomNumberGenerator;
        private int _actorsIntelligentFixation;
        private int _numberOfStates;
        private int _numberOfActions;
        private IExplorationPolicy _explorationPolicy;

        [TestFixtureSetUp]
        public void WhenMakingIntelligentFixation()
        {
            _numberOfStates = _state.Length;
            _numberOfActions = _state.Length;
            
            var _qlearning = new Mock<QLearning>(_numberOfStates, _numberOfActions, _explorationPolicy);
             var actor = new IntelligentActor(_randomNumberGenerator.Object);
            _actorsIntelligentFixation = actor.IntelligentFixation(_state);
        }

        [Test]
        public void ThenNextActionDetermined()
        {
            
        }
    }

    internal class IntelligentActor : IIntelligentActor
    {
        private IRandomNumberProvider _randomNumberProvider;

        public IntelligentActor(IRandomNumberProvider randomNumberProvider)
        {
            _randomNumberProvider = randomNumberProvider;
        }

        public int IntelligentFixation(double[] state)
        {
            throw new NotImplementedException();
        }

        public int Fixate()
        {
            return _randomNumberProvider.Take();
        }
    }
}
