using System.Collections.Generic;

namespace Framework.Data
{
    public interface IDataReader
    {
        IDictionary<int, int> GetData();
    }
}