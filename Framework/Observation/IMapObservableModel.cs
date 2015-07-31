namespace Framework.Observation
{
    public interface IMapObservableModel
    {
        void Generate();
        double[] GetState(int fixation);
    }
}