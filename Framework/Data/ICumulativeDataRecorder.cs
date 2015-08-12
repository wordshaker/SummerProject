namespace Framework.Data
{
    public interface ICumulativeDataRecorder
    {
        void Insert(int reward);
        void IncrementTrial(int currentTrial);
    }
}