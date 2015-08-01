namespace Framework.Belief_State
{
    public interface IBeliefState
    {
        void Initialise();
        bool Update(double[] activation, int fixation);
    }
}