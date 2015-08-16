using AForge.MachineLearning;
using Framework.TrialRunners;

/*
 * Need to change this to an interface and delete QLearning Class
 */

namespace Framework
{
    public class QLearningWrapper : QLearning, IQLearning
    {
        public QLearningWrapper(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize)
            : base(states, actions, explorationPolicy, randomize)
        {
        }

        public QLearningWrapper(int states, int actions, IExplorationPolicy explorationPolicy)
            : base(states, actions, explorationPolicy)
        {
        }

        public void Reward(int previousState, int action, double reward, int nextState)
        {
            base.UpdateState(previousState, action, reward, nextState);
        }

        public int GetAction(int state)
        {
            return base.GetAction(state);
        }

        public IExplorationPolicy GetPolicy()
        {
            return ExplorationPolicy;
        }
    }
}