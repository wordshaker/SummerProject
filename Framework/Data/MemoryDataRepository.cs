using System.Collections.Generic;

namespace Framework.Data
{
    public class MemoryDataRepository : IDataRecorder, IDataReader
    {
        private readonly IDictionary<int, int> _numberOfTrialsPerFixation;
        private double _totalFixations;
        private double _totalTrials;

        public MemoryDataRepository()
        {
            _numberOfTrialsPerFixation = new SortedDictionary<int, int>();
        }

        public IDictionary<int, int> GetData()
        {
            return _numberOfTrialsPerFixation;
        }

        public void Insert(int fixations)
        {
            _totalFixations += fixations;
            _totalTrials++;

            if (_numberOfTrialsPerFixation.ContainsKey(fixations))
            {
                _numberOfTrialsPerFixation[fixations] += 1;
            }
            else
            {
                _numberOfTrialsPerFixation.Add(fixations, 1);
            }
        }

        public double GetAverage()
        {
            return _totalFixations/_totalTrials;
        }
    }
}