using System;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralList<T>
{
    private readonly float defaultExpiryTime;
    public List<(T reference, float expiryTime)> References { get; } = new();

    public EphemeralList(float defaultExpiryTime = 0)
    {
        this.defaultExpiryTime = defaultExpiryTime;
    }

    public void Add(T reference, float expiryTime = 0)
    {
        if (expiryTime == 0)
        {
            expiryTime = defaultExpiryTime;
        }

        References.Add((reference, expiryTime));
    }

    public void Poll(Action<T> callback = null, Action<T> expiryCallback = null)
    {
        float time = Time.time;
        References.RemoveAll(((T reference, float expiryTime) i) =>
        {
            if (time >= i.expiryTime)
            {
                expiryCallback?.Invoke(i.reference);
                return true;
            }
            else
            {
                callback?.Invoke(i.reference);
                return false;
            }
        });
    }
}