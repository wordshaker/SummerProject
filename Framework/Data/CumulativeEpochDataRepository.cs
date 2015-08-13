using System.Collections.Generic;

namespace Framework.Data
{
    public class CumulativeEpochDataRepository : ICumulativeEpochDataRecorder, IEpochDataReader
    {
        private readonly IDictionary<int, double> _cumulativeRewardPerEpoch;
        private double _cumulativeTotalReward;

        public CumulativeEpochDataRepository()
        {
            _cumulativeRewardPerEpoch = new SortedDictionary<int, double>();
            _cumulativeTotalReward = 0;
        }

        public void Insert(double totalReward)
        {
            _cumulativeTotalReward += totalReward;
        }

        public void IncrementTrial(int currentTrial)
        {
            var currentEpoch = currentTrial/150;
            _cumulativeRewardPerEpoch.Add(currentEpoch, _cumulativeTotalReward / (currentTrial*150));
            //_cumulativeTotalReward = 0;
        }

        public IDictionary<int, double> GetData()
        {
            return _cumulativeRewardPerEpoch;
        }
    }
}