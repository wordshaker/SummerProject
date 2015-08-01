namespace Framework.Belief_State
{
    public interface IBeliefStateForMap
    {
        void Initialise();
        double[] CalculateState(double[] activation, int fixation);
    }
}