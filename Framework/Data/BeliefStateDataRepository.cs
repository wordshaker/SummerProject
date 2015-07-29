using System.Collections.Generic;

namespace Framework.Data
{
    public class BeliefStateDataRepository : IBubbleDataRecorder, IBubbleDataReader
    {
        private readonly IDictionary<int, double[]> _beliefStatePerFixation;

        public BeliefStateDataRepository()
        {
            _beliefStatePerFixation = new SortedDictionary<int, double[]>();
        }

        public IDictionary<int, double[]> GetData()
        {
            return _beliefStatePerFixation;
        }

        public void Insert(int fixation, double[] beliefStates)
        {
            _beliefStatePerFixation.Add(fixation, beliefStates);
        }
    }
}