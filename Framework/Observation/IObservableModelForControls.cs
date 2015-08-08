namespace Framework.Observation
{
    public interface IObservableModelForControls
    {
        void Generate();
        bool Update(int fixation);
    }
}