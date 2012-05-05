using System;
using System.Collections.Generic;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public static class RandomExtensions
    {
        public static int RandomIndexForListCount(this Random random, int listSize)
        {
            return ((int) (random.NextDouble()*listSize));
        }
    }
}