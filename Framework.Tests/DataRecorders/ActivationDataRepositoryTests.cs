using System.Collections.Generic;
using System.Linq;
using Framework.Data;
using NUnit.Framework;

namespace Framework.Tests.DataRecorders
{
    [TestFixture]
    public class ActivationDataRepositoryTests
    {
        private double[] _activations;
        private IDictionary<int, double[]> _activationsPerFixation;
        private int _fixation;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixation = 0;
            _activations = new[] {0.75, 0.9, 0.75, 0.25, 0.1, 0.0, 0.0};

            var activationDataRecorder = new ActivationDataRepository();
            activationDataRecorder.Insert(_fixation, _activations);
            _activationsPerFixation = activationDataRecorder.GetData();
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedAmountOfKeys()
        {
            Assert.That(_activationsPerFixation.Keys.Count, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedKey()
        {
            Assert.That(_activationsPerFixation.Keys.First(), Is.EqualTo(_fixation));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedValue()
        {
            Assert.That(_activationsPerFixation.Values.First(), Is.EqualTo(_activations));
        }
    }

    [TestFixture]
    public class ActivationDataRepositoryWithDuplicateInsertsTests
    {
        private double[] _activationsOne;
        private IDictionary<int, double[]> _activationsPerFixation;
        private int _fixationOne;
        private int _fixationTwo;
        private double[] _activationsTwo;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixationOne = 1;
            _fixationTwo = 2;
            _activationsOne = new[] {0.6, 0.8, 0.6, 0.1, 0.1, 0.0, 0.0};
            _activationsTwo = new[] { 0.75, 0.9, 0.75, 0.25, 0.1, 0.0, 0.0 };


            var activationDataRecorder = new ActivationDataRepository();
            activationDataRecorder.Insert(_fixationOne, _activationsOne);
            activationDataRecorder.Insert(_fixationTwo, _activationsTwo);
            _activationsPerFixation = activationDataRecorder.GetData();
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedFirstKey()
        {
            Assert.That(_activationsPerFixation.ContainsKey(_fixationOne));
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedSecondKey()
        {
            Assert.That(_activationsPerFixation.ContainsKey(_fixationTwo));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValueForFirstFixation()
        {
            Assert.That(_activationsPerFixation[_fixationOne], Is.EqualTo(_activationsOne));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValueForSecondFixation()
        {
            Assert.That(_activationsPerFixation[_fixationTwo], Is.EqualTo(_activationsTwo));
        }

        [Test]
        public void ThenDictionaryWithTwoEntriesHasExpectedNumberOfKeys()
        {
            Assert.That(_activationsPerFixation.Count, Is.EqualTo(2));
        }
    }
}