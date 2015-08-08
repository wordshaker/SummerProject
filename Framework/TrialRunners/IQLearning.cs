namespace Framework.TrialRunners
{
    public interface IQLearning
    {
        void UpdateState(int previousState, int action, double reward, int nextState);
        int GetAction(int state);
    }
}