using System;
using System.Collections.Generic;
using System.Linq;

public class RandomUtils
{
    public static K WeightedChoice<K>(IEnumerable<KeyValuePair<K, int>> weights)
    {
        var weightsList = weights.ToList();
        int weightsTotal = weightsList.Sum(p => p.Value);

        int roll = Random.Shared.Next(weightsTotal);
        int cumulative = 0;

        foreach ((K? key, int weight) in weights)
        {
            cumulative += weight;
            if (roll < cumulative)
            {
                return key;
            }
        }

        return weights.First().Key;
    }

    public static T WeightedChoice<T>(IEnumerable<T> values, IEnumerable<int> weights)
    {
        if (weights.Count() != values.Count())
            throw new Exception("Values and weights must be of same length.");

        return WeightedChoice(values.Zip(weights).Select(tuple => KeyValuePair.Create(tuple.First, tuple.Second)));
    }

    public static T WeightedChoice<T>(IEnumerable<T> values, Func<T, int> selector)
        => WeightedChoice(values, values.Select(selector));
}
