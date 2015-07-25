using System.Collections.Generic;

namespace Framework.Data
{
    public interface IBubbleDataReader
    {
        IDictionary<int, double[]> GetData();
    }
}