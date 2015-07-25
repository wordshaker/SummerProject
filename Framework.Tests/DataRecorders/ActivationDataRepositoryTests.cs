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
            Assert.That(_activationsPerFixation.Keys.Count, Is.EqualTo(7));
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
        private double[] _activations;
        private IDictionary<int, double[]> _activationsPerFixation;
        private int _fixation;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixation = 1;
            _activations = new[] {0.75, 0.9, 0.75, 0.25, 0.1, 0.0, 0.0};

            var activationDataRecorder = new ActivationDataRepository();
            activationDataRecorder.Insert(_fixation, _activations);
            _activationsPerFixation = activationDataRecorder.GetData();
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedKey()
        {
            Assert.That(_activationsPerFixation.ContainsKey(_fixation));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValue()
        {
            Assert.That(_activationsPerFixation[_fixation], Is.EqualTo(_activations));
        }

        [Test]
        public void ThenDictionaryWithTwoEntriesHasExpectedNumberOfKeys()
        {
            Assert.That(_activationsPerFixation.Count, Is.EqualTo(7));
        }
    }
}