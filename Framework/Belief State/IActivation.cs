namespace Framework.Belief_State
{
    public interface IActivation
    {
        double[] GenerateActivation(int fixation, int[] visualArray);
    }
}