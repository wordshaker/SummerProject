using System;
using System.Collections.Generic;
using System.Linq;
using Framework.TrialRunners;

namespace Framework
{
    public class QLearningImplementation : IQLearning
    {
        private readonly double _epsilon, _gamma;
        private readonly Dictionary<int, Dictionary<int, double>> _q;

        public QLearningImplementation(double epsilon, double gamma, int states, int actions)
        {
            _epsilon = epsilon;
            _gamma = gamma;
            _q = new Dictionary<int, Dictionary<int, double>>(states)
            {
                {
                    0, new Dictionary<int, double>(actions)
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                    }
                },
                {
                    1, new Dictionary<int, double>(actions)
                    {
                        {0, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                    }
                },
                {
                    2, new Dictionary<int, double>(actions)
                    {
                        {0, 0},
                        {1, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                    }
                },
                {
                    3, new Dictionary<int, double>(actions)
                    {
                        {0, 0},
                        {1, 0},
                        {2, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                    }
                },
                {
                    4, new Dictionary<int, double>(actions)
                    {
                        {0, 0},
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {5, 0},
                        {6, 0},
                    }
                },
                {
                    5, new Dictionary<int, double>(actions)
                    {
                        {0, 0},
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {6, 0},
                    }
                },
                {
                    6, new Dictionary<int, double>(actions)
                    {
                        {0, 0},
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                    }
                },
            };
        }

        public void Reward(int oldState, int actionTaken, double reward, int newState)
        {
            _q[oldState][actionTaken] = reward + (_gamma * _q[newState].Values.Max());
        }

        public int GetAction(int state)
        {
            var checkRandom = new Random().NextDouble();
            if (checkRandom < _epsilon) //Epsilon 
            {
                var random = GetRandom(_q[state].Count - 1);
                if (random == state)
                {
                    return random + 1;
                }
                return random;
            }

            var maxQValue = _q[state].Values.Max();
            var actions = _q[state].Where(q => q.Value == maxQValue).ToArray();

            if (actions.Length == 1) return actions[0].Key;
            var actionIndex = GetRandom(actions.Length - 1);
            return actions[actionIndex].Key;
        }

        private int GetRandom(int max)
        {
            var number = new Random();
            return number.Next(0, max);
        }
    }
}