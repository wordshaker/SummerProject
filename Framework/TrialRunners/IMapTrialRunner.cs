namespace Framework.TrialRunners
{
    public interface IMapTrialRunner
    {
        void Run();
        int GetMaxBelief(double[] _state);
    }
}