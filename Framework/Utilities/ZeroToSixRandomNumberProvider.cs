using System;

namespace Framework.Utilities
{
    /**
     * Random Number provider used to make first random fixation
     */

    public class ZeroToSixRandomNumberProvider : IRandomNumberProvider
    {
        private readonly Random _random;

        public ZeroToSixRandomNumberProvider()
        {
            _random = new Random();
        }

        public int Take()
        {
            // return _random.Next(0, 6);
            //return _random.Next(0, 69);
            return _random.Next(0, 6);
        }
    }
}