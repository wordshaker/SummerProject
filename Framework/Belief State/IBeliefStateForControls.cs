namespace Framework.Belief_State
{
    public interface IBeliefStateForControls
    {
        void Initialise();
        bool Update(double[] activation, int fixation);
    }
}