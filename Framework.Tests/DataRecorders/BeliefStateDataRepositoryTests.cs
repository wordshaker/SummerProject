using System.Collections.Generic;
using System.Linq;
using Framework.Data;
using NUnit.Framework;

namespace Framework.Tests.DataRecorders
{
    [TestFixture]
    public class BeliefStateDataRepositoryTests
    {
        private double[] _beliefs;
        private IDictionary<int, double[]> _beliefStatePerFixation;
        private int _fixation;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixation = 0;
            _beliefs = new[] {0.75, 0.9, 0.75, 0.25, 0.1, 0.0, 0.0};

            var beliefStateDataRecorder = new BeliefStateDataRepository();
            beliefStateDataRecorder.Insert(_fixation, _beliefs);
            _beliefStatePerFixation = beliefStateDataRecorder.GetData();
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedAmountOfKeys()
        {
            Assert.That(_beliefStatePerFixation.Keys.Count, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedKey()
        {
            Assert.That(_beliefStatePerFixation.Keys.First(), Is.EqualTo(_fixation));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedValue()
        {
            Assert.That(_beliefStatePerFixation.Values.First(), Is.EqualTo(_beliefs));
        }
    }

    [TestFixture]
    public class BeliefStateDataRepositoryWithDuplicateInsertsTests
    {
        private double[] _beliefsOne;
        private IDictionary<int, double[]> _beliefStatePerFixation;
        private int _fixationOne;
        private int _fixationTwo;
        private double[] _beliefsTwo;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixationOne = 1;
            _fixationTwo = 2;
            _beliefsOne = new[] {0.6, 0.8, 0.6, 0.2, 0.1, 0.0, 0.0};
            _beliefsTwo = new[] {0.75, 0.9, 0.75, 0.25, 0.1, 0.0, 0.0 };

            var beliefStateDataRecorder = new BeliefStateDataRepository();
            beliefStateDataRecorder.Insert(_fixationOne, _beliefsOne);
            beliefStateDataRecorder.Insert(_fixationTwo, _beliefsTwo);
            _beliefStatePerFixation = beliefStateDataRecorder.GetData();
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedFirstKey()
        {
            Assert.That(_beliefStatePerFixation.ContainsKey(_fixationOne));
        }
        [Test]
        public void ThenDictionaryContainsTheExpectedSecondKey()
        {
            Assert.That(_beliefStatePerFixation.ContainsKey(_fixationTwo));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValueForTheFirstKey()
        {
            Assert.That(_beliefStatePerFixation[_fixationOne], Is.EqualTo(_beliefsOne));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValueForTheSecondKey()
        {
            Assert.That(_beliefStatePerFixation[_fixationTwo], Is.EqualTo(_beliefsTwo));
        }

        [Test]
        public void ThenDictionaryWithTwoEntriesHasExpectedNumberOfKeys()
        {
            Assert.That(_beliefStatePerFixation.Count, Is.EqualTo(2));
        }
    }
}