using System.Collections.Generic;

namespace Framework.Data
{
    public class AnalysisDataRepository : IBubbleDataRecorder, IBubbleDataReader
    {
        private readonly IDictionary<int, double[]> _statePerFixation;

        public AnalysisDataRepository()
        {
            _statePerFixation = new SortedDictionary<int, double[]>();
        }

        public IDictionary<int, double[]> GetData()
        {
            return _statePerFixation;
        }

        public void Insert(int fixation, double[] state)
        {
            _statePerFixation.Add(fixation, state);
        }
    }
}