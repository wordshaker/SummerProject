namespace Framework.Observation
{
    public interface IObservableModel
    {
        void Generate();
        bool Update(int fixation);
    }
}