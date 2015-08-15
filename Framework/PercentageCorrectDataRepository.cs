using System.Collections.Generic;
using Framework.Data;

namespace Framework
{
    public class PercentageCorrectDataRepository : IDataRecorder, ICorrectnessDataReader
    {
        private readonly IDictionary<int, int> _numberOfTrialsPerFixation;
        private double _totalFixations;
        private double _totalTrials;
        private readonly IDictionary<int, double> _percentageCorrect;

        public PercentageCorrectDataRepository()
        {
            _numberOfTrialsPerFixation = new SortedDictionary<int, int>();
            _percentageCorrect = new SortedDictionary<int, double>();

        }

        public IDictionary<int, double> GetData()
        {
            var runningTotal = 0;
            for (var i = 0; i < 13; i++)
            {
                if (_numberOfTrialsPerFixation.ContainsKey(i))
                {
                    runningTotal += _numberOfTrialsPerFixation[i];
                }

                var correctnessAtFixation = runningTotal / _totalTrials;
                _percentageCorrect.Add(i, correctnessAtFixation);
            }
            return _percentageCorrect;
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