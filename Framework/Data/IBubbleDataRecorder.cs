namespace Framework.Data
{
    public interface IBubbleDataRecorder
    {
        void Insert(int fixation, double[] activations);
    }
}