namespace Framework.Belief_State
{
    public interface IBeliefState
    {
        void Initialise();
        double[] CalculateState(double[] activation, int fixation);
    }
}