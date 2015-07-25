using System;

namespace Framework.Utilities
{
    public class ZeroToSixRandomNumberProvider : IRandomNumberProvider
    {
        private readonly Random _random;

        public ZeroToSixRandomNumberProvider()
        {
            _random = new Random();
        }

        public int Take()
        {
            return _random.Next(0, 6);
        }
    }
}