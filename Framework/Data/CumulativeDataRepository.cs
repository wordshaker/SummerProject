using System.Collections.Generic;

namespace Framework.Data
{
    public class CumulativeDataRepository : ICumulativeDataRecorder, IDataReader
    {
        private readonly IDictionary<int, int> _cumulativeRewardPerTestTrial;
        private int _cumulativeReward;

        public CumulativeDataRepository()
        {
            _cumulativeRewardPerTestTrial = new SortedDictionary<int, int>();
            _cumulativeReward = 0;
        }

        public void Insert(int reward)
        {
            _cumulativeReward += reward;
        }

        public void IncrementTrial(int currentTrial)
        {
            _cumulativeRewardPerTestTrial.Add(currentTrial/150, _cumulativeReward);
            _cumulativeReward = 0;
        }

        public IDictionary<int, int> GetData()
        {
            return _cumulativeRewardPerTestTrial;
        }
    }
}