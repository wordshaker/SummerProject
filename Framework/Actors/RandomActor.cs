using System.Collections.Generic;
using Framework.Utilities;

namespace Framework.Actors
{
    public class RandomActor : IActor
    {
        private readonly IRandomNumberProvider _randomNumberProvider;
        private List<int> _fixations = new List<int>();

        public RandomActor(IRandomNumberProvider randomNumberProvider)
        {
            _randomNumberProvider = randomNumberProvider;
        }

        public int Fixate()
        {
            return _randomNumberProvider.Take();
        }
    }
}