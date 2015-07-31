namespace Framework.Belief_State
{
    public interface IBeliefState
    {
        void Initialise();
        bool Update(double[] activation, int fixation);
        double[] CalculateState(double[] activation, int fixation);
    }
}