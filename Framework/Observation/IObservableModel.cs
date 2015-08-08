namespace Framework.Observation
{
    public interface IObservableModel
    {
        void Generate();
        double[] GetState(int fixation);
    }
}