using Framework.Data;
using Framework.Observation;
using Framework.Utilities;
using Moq;
using NUnit.Framework;

namespace Framework.Tests
{
   [TestFixture]
    public class QlearningExperimentTests
    {
        private readonly int _numberOfTrials = 200;
       private Mock<ICumulativeDataRecorder> _dataRecorder;

       [TestFixtureSetUp]
        public void WhenRunTrialsIsCalled()
        {
            _dataRecorder = new Mock<ICumulativeDataRecorder>();

           var state = new double[7];
           for (var i = 0; i < 7; i++)
           {
               state[i] = 0.9;
           }

           var observableModel = new Mock<IObservableModel>();
           observableModel
               .Setup(o => o.GetState(It.IsAny<int>()))
               .Returns(state);

           var experiment = new QLearningExperiment(observableModel.Object, new Mock<IRandomNumberProvider>().Object, _dataRecorder.Object);
            experiment.RunTrials(_numberOfTrials);
        }

        [Test]
        public void ThenTheCumulativeDataRecorderIsIncrementedOnce()
        {
            _dataRecorder.Verify(d => d.IncrementTrial(150));
        }
    }
}