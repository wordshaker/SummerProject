using System.Collections.Generic;

namespace Framework.Data
{
    public interface ICorrectnessDataReader
    {
        IDictionary<int, double> GetData();
    }
}
