using System.Collections.Generic;
using System.Linq;
using Framework.Data;
using NUnit.Framework;

namespace Framework.Tests.DataRecorders
{
    [TestFixture]
    public class MemoryDataRepositoryTests
    {
        private IDictionary<int, int> _numberOfTrialsAndFixations;
        private int _fixations;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixations = 6;

            var memoryDataRecorder = new MemoryDataRepository();
            memoryDataRecorder.Insert(_fixations);
            _numberOfTrialsAndFixations = memoryDataRecorder.GetData();
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedAmountOfKeys()
        {
            Assert.That(_numberOfTrialsAndFixations.Keys.Count, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedKey()
        {
            Assert.That(_numberOfTrialsAndFixations.Keys.First(), Is.EqualTo(_fixations));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedValue()
        {
            Assert.That(_numberOfTrialsAndFixations.Values.First(), Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class MemoryDataRepositoryWithDuplicateInsertsTests
    {
        private int _fixations;
        private IDictionary<int, int> _numberOfTrialsAndFixations;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _fixations = 6;

            var memoryDataRecorder = new MemoryDataRepository();
            memoryDataRecorder.Insert(_fixations);
            memoryDataRecorder.Insert(_fixations);

            _numberOfTrialsAndFixations = memoryDataRecorder.GetData();
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedKey()
        {
            Assert.That(_numberOfTrialsAndFixations.ContainsKey(_fixations));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValue()
        {
            Assert.That(_numberOfTrialsAndFixations[6], Is.EqualTo(2));
        }

        [Test]
        public void ThenDictionaryWithTwoEntriesHasExpectedNumberOfKeys()
        {
            Assert.That(_numberOfTrialsAndFixations.Count, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class MemoryDataRepositoryWithMultipleInsertsTests
    {
        private double _average;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetAverageIsCalled()
        {
            const int firstFixations = 3;
            const int secondFixations = 4;

            var memoryDataRecorder = new MemoryDataRepository();
            memoryDataRecorder.Insert(firstFixations);
            memoryDataRecorder.Insert(secondFixations);

            _average = memoryDataRecorder.GetAverage();
        }

        [Test]
        public void ThenTheExpectedAverageIsReturned()
        {
            Assert.That(_average, Is.EqualTo(3.5));
        }
    }
}