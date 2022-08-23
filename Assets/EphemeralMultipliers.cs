using System;
using UnityEngine;

public class EphemeralMultipliers
{
    private readonly EphemeralList<Func<float, float>> multipliers = new();

    public void Add(float multiplier, float expiryTime)
    {
        multipliers.Add(t => multiplier, expiryTime);
    }

    public void Add(Func<float, float> multiplier, float expiryTime)
    {
        multipliers.Add(multiplier, expiryTime);
    }

    public float GetProduct(float operand = 1f)
    {
        float product = operand;
        /*multipliers.Poll(multiplier, time =>
        {
            product *= multiplier(time);
        });*/
        return product;
    }
}