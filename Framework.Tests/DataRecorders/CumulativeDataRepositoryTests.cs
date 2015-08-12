using System.Collections.Generic;
using System.Linq;
using Framework.Data;
using NUnit.Framework;

namespace Framework.Tests.DataRecorders
{
    internal class CumulativeDataRepositoryTests
    {
        private IDictionary<int, int> _cumulativeRewardPerTestTrial;
        private int _currentTrial;
        private int _reward;

        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _currentTrial = 150;
            _reward = -5;

            var cumulativeDataRecorder = new CumulativeDataRepository();
            cumulativeDataRecorder.IncrementTrial(_currentTrial);
            cumulativeDataRecorder.Insert(_reward);
            _cumulativeRewardPerTestTrial = cumulativeDataRecorder.GetData();
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedAmountOfKeys()
        {
            Assert.That(_cumulativeRewardPerTestTrial.Keys.Count, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedKey()
        {
            Assert.That(_cumulativeRewardPerTestTrial.Keys.First(), Is.EqualTo(_currentTrial/150));
        }

        [Test]
        public void ThenTheDictionaryHasTheExpectedValue()
        {
            Assert.That(_cumulativeRewardPerTestTrial.Values.First(), Is.EqualTo(_reward));
        }
    }

    [TestFixture]
    public class CumulativeDataRepositoryWithTwoEntriesTests
    {
        private IDictionary<int, int> _cumulativeRewardPerTestTrial;
        private int _firstTrial;
        private int _secondTrial;
        private int _rewardOne;
        private int _rewardTwo;


        [TestFixtureSetUp]
        public void WhenDataIsRecordedAndGetDataIsCalled()
        {
            _firstTrial = 150;
            _secondTrial = 300;

            _rewardOne = -5;
            _rewardTwo = 10;

            var cumulativeDataRecorder = new CumulativeDataRepository();
            cumulativeDataRecorder.IncrementTrial(_firstTrial);
            cumulativeDataRecorder.Insert(_rewardOne);
            cumulativeDataRecorder.IncrementTrial(_secondTrial);
            cumulativeDataRecorder.Insert(_rewardTwo);
            _cumulativeRewardPerTestTrial = cumulativeDataRecorder.GetData();
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedFirstKey()
        {
            Assert.That(_cumulativeRewardPerTestTrial.ContainsKey(_firstTrial/150));
        }

        [Test]
        public void ThenDictionaryContainsTheExpectedSecondKey()
        {
            Assert.That(_cumulativeRewardPerTestTrial.ContainsKey(_secondTrial/150));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValueForFirstFixation()
        {
            Assert.That(_cumulativeRewardPerTestTrial[_firstTrial/150], Is.EqualTo(_rewardOne));
        }

        [Test]
        public void ThenDictionaryHasTheExpectedValueForSecondFixation()
        {
            Assert.That(_cumulativeRewardPerTestTrial[_secondTrial/150], Is.EqualTo(_rewardTwo));
        }

        [Test]
        public void ThenDictionaryWithTwoEntriesHasExpectedNumberOfKeys()
        {
            Assert.That(_cumulativeRewardPerTestTrial.Count, Is.EqualTo(2));
        }
    }
}