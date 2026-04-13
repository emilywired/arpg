using System;
using System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static T WeightedChoice<T>(this IEnumerable<T> enumerable, Func<T, int> selector)
        => RandomUtils.WeightedChoice(enumerable, selector);
}