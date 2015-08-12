using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Actors;
using Framework.Data;
using Framework.Observation;
using Framework.TrialRunners;
using Framework.Utilities;
using Moq;
using NUnit.Framework;

namespace Framework.Tests.TrialRunners
{
    /*[TestFixture]
     public class QLearningTrialRunnerTests
     {
         private Mock<IObservableModel> _observableModel;
         private Mock<IRandomNumberProvider> _randomNumberProvider;
         private Mock<IQLearning> _learning;

         private double[] _firstState;
         private Mock<IQLearningActor> _actor;
        private int _fixationLocation;

        [TestFixtureSetUp]
         public void WhenInitialisingAQLearningTrial()
         {
             _firstState = new[] {0.25, 0.35, 0.25, 0.1, 0.4, 0.7, 0.0};
             var secondState = new[] {0.25, 0.60, 0.25, 0.8, 0.0, 0.0, 0.0};
             var thirdState = new[] {0.25, 0.9, 0.25, 0.1, 0.0, 0.0, 0.0};
             var stateQueue = new Queue<double[]>(new[] {_firstState, secondState, thirdState});

            _actor = new Mock<IQLearningActor>();
            _actor.Setup(());

             var tabuPolicy = new Mock<TestTabuSearchExploration>().Object;
             _learning = new Mock<IQLearning>();
             _learning
                 .Setup(l => l.GetPolicy())
                 .Returns(tabuPolicy);
        
             _randomNumberProvider = new Mock<IRandomNumberProvider>();

             _observableModel = new Mock<IObservableModel>();
             _observableModel
                 .Setup(o => o.GetState(It.IsAny<int>()))
                 .Returns(stateQueue.Dequeue);

             var trial = new QLearningTrialRunner(_observableModel.Object, _randomNumberProvider.Object, () => _actor.Object);
             trial.Run(_learning.Object);
         }

         [Test]
         public void ThenObservableModelGenerated()
         {
             _observableModel.Verify(o => o.Generate());
         }

         [Test]
         public void ThenRandomNumberGenerated()
         {
             _randomNumberProvider.Verify(r => r.Take());
         }

         [Test]
         public void ThenTheRecorderIsCalledWithTheNumberOfFixations()
         {
             _dataRecorder.Verify(d => d.Insert(3));
         }       
        
         [Test]
         public void ThenStateGeneratedUsingFirstFixation()
         {
             _observableModel.Verify(o => o.GetState(It.IsAny<int>()));
         }
     }*/
}
