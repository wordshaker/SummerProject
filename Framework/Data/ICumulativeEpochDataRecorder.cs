namespace Framework.Data
{
    public interface ICumulativeEpochDataRecorder
    {
        void Insert(double totalReward);
        void IncrementTrial(int currentTrial);
    }
}