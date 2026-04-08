using System;
using System.Collections.Generic;
using System.Linq;

public class RandomUtils
{
    public static int WeightedIndex(IEnumerable<int> weights)
    {
        var weightsList = weights.ToList();
        int weightsTotal = weightsList.Sum();

        int roll = Random.Shared.Next(weightsTotal);
        int cumulative = 0;

        for (int i = 0; i < weights.Count(); i++)
        {
            cumulative += weightsList[i];
            if (roll < cumulative)
            {
                return i;
            }
        }

        return weights.Count() - 1;
    }

    public static T WeightedChoice<T>(IEnumerable<T> values, IEnumerable<int> weights)
    {
        if (weights.Count() != values.Count())
            throw new Exception("Values and weights must be of same length.");

        int i = RandomUtils.WeightedIndex(weights);
        return values.ElementAt(i);
    }
}
