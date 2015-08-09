using Framework.Utilities;

namespace Framework.VisualArray
{
    public class VisualArrayGenerator : IVisualArrayGenerator
    {
        private readonly IRandomNumberProvider _randomNumberProvider;

        public VisualArrayGenerator(IRandomNumberProvider randomNumberProvider)
        {
            _randomNumberProvider = randomNumberProvider;
        }

        public int[] Generate()
        {
            var location = _randomNumberProvider.Take();
            //var array = new[] {0, 0, 0, 0, 0, 0, 0};
            var array = new int[70];
            array[location] = 1;
            return array;
        }
    }
}