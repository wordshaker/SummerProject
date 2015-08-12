using AForge.MachineLearning;
using Framework.TrialRunners;

/*
 * QLearning and IExplorationPolicy from:
 * http://www.aforgenet.com/framework/docs/html/5b599b12-9e4b-9d0c-40e4-88cec36b9327.htm
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