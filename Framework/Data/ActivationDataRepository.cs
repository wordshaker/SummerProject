using System;
using System.Collections.Generic;

namespace Framework.Data
{
    public class ActivationDataRepository : IBubbleDataRecorder, IBubbleDataReader
    {
        private readonly IDictionary<int, double[]> _activationsPerFixation;

        public ActivationDataRepository()
        {
            _activationsPerFixation = new SortedDictionary<int, double[]>();
        }

        public IDictionary<int, double[]> GetData()
        {
            return _activationsPerFixation;
        }

        public void Insert(int fixation, double[] activations)
        {
            _activationsPerFixation.Add(fixation, activations);
        }
    }
}