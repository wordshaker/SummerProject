using System.Collections.Generic;

namespace Framework.Data
{
    public interface IEpochDataReader
    {
        IDictionary<int, double> GetData();
    }
}