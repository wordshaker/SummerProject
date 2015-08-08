using AForge.MachineLearning;
using Framework.TrialRunners;

namespace Framework
{
    public class QLearningWrapper : QLearning, IQLearning
    {
        public QLearningWrapper(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize) : base(states, actions, explorationPolicy, randomize)
        {
        }

        public QLearningWrapper(int states, int actions, IExplorationPolicy explorationPolicy) : base(states, actions, explorationPolicy)
        {
        }

        public void UpdateState(int previousState, int action, double reward, int nextState)
        {
            base.UpdateState(previousState, action, reward, nextState);
        }

        public int GetAction(int state)
        {
            return base.GetAction(state);
        }
    }
}